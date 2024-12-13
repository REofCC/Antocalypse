using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Worker : MonoBehaviour
{

    private BTSelector root;

    EntityData entityData;
    CapsuleCollider2D collider;

    [SerializeField]
    GameObject cargo;

    Vector2 cargoPos;
    Vector2 nodePos;

    TaskType currentTask;
    State state;
    //State nextState;
    Vector2 currentTargetPos;
    Vector2 targetPos;
    Vector2Int targetGridPos;
    HexaMapNode targetNode;
    List<Vector3> path;
    int pathIndex;
    BuildingType buildingType;

    public float buildTime = 2f;    //임시 건설시간
    public float gatherTime = 2f;    //임시 채집시간

    // ToDo : 건설 명령 받을 때 건설시간 받아야 함
    private void Awake()
    {
        entityData = GetComponent<EntityData>();
        collider = GetComponent<CapsuleCollider2D>();
        state = State.Idle;
        cargoPos = cargo.transform.position;
        currentTask = TaskType.None;
    }
    private void Start()
    {
        root = new BTSelector();
        BTSelector orderSelector = new BTSelector();

        BTSequence eatSequence = new BTSequence();
        BTSequence returnSequence = new BTSequence();
        BTSequence gatherSequence = new BTSequence();
        BTSequence buildSequence = new BTSequence();

        BTAction eatAction = new BTAction(Eat);
        BTAction moveAction = new BTAction(Move);
        BTAction storeAction = new BTAction(Store);
        BTAction gatherAction = new BTAction(GatherResource);
        BTAction buildAction = new BTAction(Build);
        BTAction idleAction = new BTAction(Idle);

        BTCondition kcalLow = new BTCondition(IsKcalLow);
        BTCondition isHolding = new BTCondition(IsHolding);
        BTCondition isGatherOrder = new BTCondition(IsGatherOrder);
        BTCondition isBuildOrder = new BTCondition(IsBuildOrder);


        root.AddChild(eatSequence);
        root.AddChild(orderSelector);
        root.AddChild(idleAction);

        eatSequence.AddChild(kcalLow);
        eatSequence.AddChild(moveAction);
        eatSequence.AddChild(eatAction);

        orderSelector.AddChild(returnSequence);
        orderSelector.AddChild(gatherSequence);
        orderSelector.AddChild(buildSequence);

        returnSequence.AddChild(isHolding);
        returnSequence.AddChild(moveAction);
        returnSequence.AddChild(storeAction);

        gatherSequence.AddChild(isGatherOrder);
        gatherSequence.AddChild(moveAction);
        gatherSequence.AddChild(gatherAction);

        buildSequence.AddChild(isBuildOrder);
        buildSequence.AddChild(moveAction);
        buildSequence.AddChild(buildAction);

        root.Evaluate();
    }
    #region BT
    #region BTAction
    BTNodeState Eat()
    {
        // 섭취 행동 대기시간 및 애니메이션
        return BTNodeState.Success;
    }
    BTNodeState Move()
    {
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

    BTNodeState Store()
    {
        entityData.isHolding = false;
        Debug.Log("Stored");
        // 자원 보관 추가
        return BTNodeState.Success;
    }
    BTNodeState GatherResource()
    {
        if (currentTask == TaskType.Gather && state != State.Gather)   // 최초 진입 시
        {
            ChangeState(State.Gather);
            // 채집 대기시간, 애니메이션
            StartCoroutine(GatherTimer());
        }
        else if (currentTask == TaskType.Gather && state != State.Gather)   // 건설 종료 후
        {
            return BTNodeState.Success;
        }
        return BTNodeState.Running;
    }
    BTNodeState Build()
    {
        if (currentTask == TaskType.Build && state != State.Build)   // 최초 진입 시
        {
            ChangeState(State.Build);
            // 건설 자원 소모 및 대기시간, 애니메이션
            StartCoroutine(BuildTimer());
        }
        else if (currentTask == TaskType.Build && state != State.Build)   // 건설 종료 후
        {
            return BTNodeState.Success;
        }
        return BTNodeState.Running;
    }
    BTNodeState Idle()
    {
        // 유휴 애니메이션
        return BTNodeState.Running;
    }
    #endregion
    #region BTCondition
    bool IsKcalLow()
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
    bool IsHolding()
    {
        if (entityData.isHolding)
        {
            targetPos = cargoPos;
            ChangeState(State.Return);
            return true;
        }
        return false;
    }
    bool IsGatherOrder()
    {
        if (currentTask == TaskType.Gather)
        {
            targetPos = nodePos;
            return true;
        }
        else
            return false;
    }
    bool IsBuildOrder()
    {
        if (currentTask == TaskType.Build)
            return true;
        else
            return false;
    }
    #endregion
#endregion

    private void FixedUpdate()
    {
        root.Evaluate();
    }
    void ChangeState(State state)
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
    public void GetTask(HexaMapNode targetNode, TaskType type)
    {
        Debug.Log("Task Confirmed");

        if (type == TaskType.Build)
        {
            RequestPath(targetNode, true);
            buildingType = BuildingType.None;
        }
        else
        {
            RequestPath(targetNode, false);
        }
        currentTask = type;
        pathIndex = path.Count - 1;
        targetPos = targetNode.GetWorldPos();
        currentTargetPos = path[pathIndex];
        targetGridPos = targetNode.GetGridPos();
    }
    public void GetTask(HexaMapNode targetNode, TaskType type, BuildingType _buildingType)
    {
        Debug.Log("Task Confirmed");

        if (type == TaskType.Build)
            buildingType = _buildingType;

        RequestPath(targetNode, false);

        currentTask = type;
        pathIndex = path.Count - 1;
        targetPos = targetNode.GetWorldPos();
        currentTargetPos = path[pathIndex];
        targetGridPos = targetNode.GetGridPos();
    }
    float RotateValue(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        return angle;
    }
    void RequestPath(HexaMapNode targetNode, bool isTargetWall)
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
    //void FindCargo(Resourcetype resourceType)
    //{
    //    LayerMask resourceLayer;    //해당 자원 레이어
    //    switch (resourceType)
    //    {
    //        case Resourcetype.Leaf:
    //            //resourceLayer = 
    //            break;
    //        case Resourcetype.Wood:
    //            //resourceLayer = 
    //            break;
    //        case Resourcetype.Liquid:
    //            //resourceLayer = 
    //            break;
    //        case Resourcetype.Solid:
    //            //resourceLayer = 
    //            break;
    //    }    
        
    //    GameObject obj = null;

    //    var hits = Physics2D.CircleCastAll(nodePos, Mathf.Infinity, Vector2.zero, Mathf.Infinity, resourceLayer);

    //    foreach (var hit in hits.OrderBy(distance => Vector2.Distance(nodePos, distance.point)))
    //    {
    //        if ((hit.collider.GetComponent<건물>().현재저장가능?()))
    //        {
    //            obj = hit.collider.gameObject;
    //            Debug.Log("Found");
    //            break;
    //        }
    //    }

    //    if (obj == null)
    //    {
    //        Debug.Log("Can't Find");
    //    }

    //    cargo = obj;
    //}

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

    IEnumerator BuildTimer()
    {
        yield return new WaitForSeconds(buildTime); 
        Debug.Log("Build Finish");
        // 작업 완료 전달?
        if (buildingType == BuildingType.None)
        {
            Wall node = (Wall)targetNode;
            MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "Path", true);
            //targetNode.SetIsWorked(false);
            //if (node.GetResource() != null)
            //{
            //    HexaMapNode resNode = MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "ResourceNode", true);
            //    MapManager.Map.ResourceFactory.SetResource(node, resNode as ResourceNode2);
            //    targetNode.SetIsWorked(false);
            //}
            //else
            //{
            //    MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "Path", true);
            //    targetNode.SetIsWorked(false);
            //}
        }
        else
        {
            //건물 건설
        }
        ChangeState(State.Idle);
        currentTask = TaskType.None;
    }
    IEnumerator GatherTimer()
    {
        yield return new WaitForSeconds(gatherTime);
        Debug.Log("Gather Finish");
        // 작업 완료 전달?
        entityData.isHolding = true;
        entityData.holdValue = entityData.gatherValue;

        //FindCargo(); //저장소 경로 할당
        ChangeState(State.Move);
    }
}
