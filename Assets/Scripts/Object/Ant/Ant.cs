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
    }
    #endregion
    #endregion
    #region BT
    #region BasicBTAction
    protected BTNodeState Eat()
    {
        // 섭취 행동 대기시간 및 애니메이션
        return BTNodeState.Success;
    }
    protected BTNodeState Move()
    {
        if (state != State.Move)
            ChangeState(State.Move);
        if (transform.position.x == currentTargetPos.x && transform.position.y == currentTargetPos.y)
        {
            //Debug.Log("Move Finish");
            //ChangeState(State.Idle);
            if (pathIndex == 0) // 경로 마지막일 때
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, RotateValue(targetPos)));
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
        transform.position = Vector2.MoveTowards(transform.position, currentTargetPos, entityData.speed * Time.deltaTime);
        return BTNodeState.Running;
    }
    protected BTNodeState Idle()
    {
        if (state != State.Idle)
            ChangeState(State.Idle);
        // 유휴 애니메이션
        return BTNodeState.Running;
    }
    #endregion
    #region BasicBTCondition
    protected bool IsKcalLow()
    {
        if (entityData.kcal <= 50 && currentTask == TaskType.None)    //수치 조정
        {
            currentTask = TaskType.Eat;
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
