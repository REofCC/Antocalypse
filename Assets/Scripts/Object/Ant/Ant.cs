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
    protected void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                this.state = State.Idle;
                break;
            case State.Move:
                this.state = State.Move;
                break;
            case State.Gather:
                this.state = State.Gather;
                break;
            case State.Return:
                this.state = State.Return;
                break;
            case State.Eat:
                this.state = State.Eat;
                break;
            case State.Build:
                this.state = State.Build;
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
    private void Awake()
    {
        state = State.Idle;
        currentTask = TaskType.None;
    }
    #endregion
    #endregion
    #region BT
    #region BasicBTAction
    protected BTNodeState Eat()
    {
        // ���� �ൿ ���ð� �� �ִϸ��̼�
        return BTNodeState.Success;
    }
    protected BTNodeState Move()
    {
        if (transform.position.x == currentTargetPos.x && transform.position.y == currentTargetPos.y)
        {
            //Debug.Log("Move Finish");
            //ChangeState(State.Idle);
            if (pathIndex == 0) // ��� �������� ��
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
        // ���� �ִϸ��̼�
        return BTNodeState.Running;
    }
    #endregion
    #region BasicBTCondition
    protected bool IsKcalLow()
    {
        if (entityData.kcal <= 50 && currentTask == TaskType.None)    //��ġ ����
        {
            currentTask = TaskType.Eat;
            //path = 
            //���հ��� or ��ü �ķ� ������ ���� ��� ��û
            return true;
        }

        else
            return false;
    }
    #endregion
    #endregion
}
