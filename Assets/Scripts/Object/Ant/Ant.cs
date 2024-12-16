using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ant : MonoBehaviour
{
    #region Basic Atrribute
    protected BTSelector root;

    protected EntityData entityData;
    protected CapsuleCollider2D collider;

    protected TaskType currentTask;
    protected State state;

    protected Vector2 nodePos;

    protected Vector2 currentTargetPos;
    protected Vector2 targetPos;
    protected Vector2Int targetGridPos;

    protected HexaMapNode targetNode;
    protected List<Vector3> path;
    protected int pathIndex;

    protected bool isCorutineRunning;

    protected float randMoveTimer;
    protected float currentTimer;

    protected Animator animator;
    #endregion
    #region BasicGetter
    public State GetCurrentState()
    {
        return state;
    }
    public TaskType GetCurrentTask()
    {
        return currentTask;
    }
    public void GetTask(TaskType task)
    {
        currentTask = task;
    }
    public int GetGatherValue()
    {
        return entityData.gatherValue;
    }
    public Vector2 GetTargetNodePos()
    {
        return nodePos;
    }
    #endregion
    #region BasicFunction
    #region Public
    #endregion
    #region Private
    protected abstract void SetBT();
    protected void ChangeState(State _state)
    {
        if (_state == state)
            return;
        switch (_state)
        {
            case State.Idle:
                state = State.Idle;
                animator.SetInteger("State", 0);
                break;
            case State.Move:
                state = State.Move;
                animator.SetInteger("State", 1);
                break;
            case State.Gather:
                state = State.Gather;
                animator.SetInteger("State", 2);
                break;
            case State.Return:
                state = State.Return;
                break;
            case State.Eat:
                state = State.Eat;
                break;
            case State.Build:
                state = State.Build;
                animator.SetInteger("State", 2);
                break;
        }
    }
    protected void RequestPath(HexaMapNode targetNode, bool isTargetWall)
    {
        HexaMapNode start = MapManager.Map.UnderGrid.GetNode(transform.position);
        if (isTargetWall)
        {
            path = MapManager.Map.UnderPathFinder.ReachWallPathFinding(start, targetNode);
        }
        else
        {
            path = MapManager.Map.UnderPathFinder.PathFinding(start, targetNode);
        }
    }
    protected float RotateValue(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return angle;
    }
    protected void RandMove()
    {
        Debug.Log("Starting Rand Move");
        HexaMapNode node = MapManager.Map.UnderGrid.GetNode(gameObject);
        targetNode = MapManager.Map.UnderGrid.GetRandomWalkableNode(node);
        RequestPath(targetNode, false);
        pathIndex = path.Count - 1;
        currentTargetPos = path[pathIndex];
        ChangeState(State.Move);
    }
    #endregion
    #region Unity
    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        entityData = GetComponent<EntityData>();
        collider = GetComponent<CapsuleCollider2D>();

        state = State.Idle;
        currentTask = TaskType.None;
        animator.SetInteger("State", 0);

        entityData.kcal = entityData.maxKcal;
        randMoveTimer = 3.0f;
        currentTimer = 0;
    }
    private void FixedUpdate()
    {
        if (entityData.kcal <= 0)
        {
            Destroy(gameObject);
            //사망
        }
        entityData.kcal -= Time.deltaTime;
    }
    #endregion
    #region Coroutine
    //IEnumerator RandMove()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    Debug.Log("Starting Rand Move");
    //    if (state == State.Idle)
    //    {
    //        HexaMapNode node = MapManager.Map.UnderGrid.GetNode(gameObject);
    //        targetNode = MapManager.Map.UnderGrid.GetRandomWalkableNode(node);
    //        RequestPath(targetNode, false);
    //        pathIndex = path.Count - 1;
    //        currentTargetPos = path[pathIndex];
    //        ChangeState(State.Move);
    //    }
    //    isCorutineRunning = false;
    //}
    #endregion
    #endregion
    #region BT
    #region BasicBTAction
    protected BTNodeState Eat()
    {
        if (Managers.Resource.CheckLiquidFood(1))
        {
            Managers.Resource.AddLiquidFood(-1);
            entityData.kcal = entityData.maxKcal;
            return BTNodeState.Success;
        }
        else
        {
            return BTNodeState.Failure;
        }
    }
    protected BTNodeState Move()
    {
        if (transform.position.x == currentTargetPos.x && transform.position.y == currentTargetPos.y)
        {
            //Debug.Log("Move Finish");
            //ChangeState(State.Idle);
            if (pathIndex == 0) // 경로 마지막일 때
            {
                if (currentTask == TaskType.Build)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotateValue(targetPos)));
                }
                if (currentTask ==TaskType.None)
                    ChangeState(State.Idle);
                return BTNodeState.Success;
            }
            else
            {
                pathIndex--;
                currentTargetPos = path[pathIndex];
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotateValue(currentTargetPos)));
                return BTNodeState.Running;
            }
        }
        else
        {
            if (state != State.Move)
                ChangeState(State.Move);
        }
        transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, entityData.speed * Time.deltaTime);
        return BTNodeState.Running;
    }
    protected BTNodeState Idle()
    {
        if (state != State.Move)  //최초 진입 시
        {
            ChangeState(State.Idle);
            if (currentTimer<randMoveTimer)
            {
                currentTimer += Time.deltaTime;
                return BTNodeState.Running;
            }
            else
            {
                currentTimer = 0;
                RandMove();
                return BTNodeState.Success;
            }
            //StartCoroutine(RandMove());
        }
        else if (state == State.Move)
        {
            return BTNodeState.Success;
        }
        return BTNodeState.Running;
    }
    #endregion
    #region BasicBTCondition
    protected bool IsKcalLow()
    {
        if (entityData.kcal <= 30)    //수치 조정
        {
            //currentTask = TaskType.Eat;
            //path = 
            //여왕개미 or 액체 식량 보관소 까지 경로 요청
            return true;
        }
        else
            return false;
    }
    #endregion
    #endregion
}
