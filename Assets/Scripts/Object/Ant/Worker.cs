using System.Collections;
using UnityEngine;

public class Worker : Ant
{
    #region Attribute
    [SerializeField]
    GameObject cargo;

    Vector2 cargoPos;

    //State nextState;

    BuildingType buildingType;

    public float wallBreakTime = 2f;    //�ӽ� �� �ı� �ð�
    public float gatherTime = 2f;    //�ӽ� ä���ð�
    #endregion
    #region Function
    #region Public
    public void GetTask(HexaMapNode _targetNode, TaskType type)
    {
        Debug.Log("Task Confirmed");

        if (type == TaskType.Build)
        {
            RequestPath(_targetNode, true);
            buildingType = BuildingType.None;
        }
        else
        {
            RequestPath(_targetNode, false);
        }
        currentTask = type;
        targetNode = _targetNode;
        pathIndex = path.Count - 1;
        targetPos = targetNode.GetWorldPos();
        currentTargetPos = path[pathIndex];
        targetGridPos = targetNode.GetGridPos();
    }
    public void GetTask(HexaMapNode _targetNode, TaskType type, BuildingType _buildingType)
    {
        Debug.Log("Task Confirmed");

        if (type == TaskType.Build)
            buildingType = _buildingType;

        RequestPath(_targetNode, false);

        currentTask = type;
        targetNode = _targetNode;
        pathIndex = path.Count - 1;
        targetPos = targetNode.GetWorldPos();
        currentTargetPos = path[pathIndex];
        targetGridPos = targetNode.GetGridPos();
    }
    #endregion
    #region Private
    protected override void SetBT()
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
    }
    //void FindCargo(Resourcetype resourceType)
    //{
    //    LayerMask resourceLayer;    //�ش� �ڿ� ���̾�
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
    //        if ((hit.collider.GetComponent<�ǹ�>().�������尡��?()))
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
    void StartBuild()
    {
        Debug.Log("Start Building");
        if (buildingType == BuildingType.None)
        {
            //MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "Path", true);
            //targetNode.SetIsWorked(false);
            StartCoroutine(WallBreakTimer());
        }
        else
        {
            MapManager.Map.BuildingFactory.Build((Path)targetNode, buildingType, OnBuildFinish);
            //�ǹ� �Ǽ�
        }
    }
    void OnBuildFinish(bool finished)
    {
        Debug.Log(finished);
        if (finished)
        {
            Debug.Log("Finished Building");
        }
        else
        {
            Debug.Log("Failed Building");
        }
        ChangeState(State.Idle);
        currentTask = TaskType.None;
    }
    #endregion
    #region Unity
    private void Awake()
    {
        entityData = GetComponent<EntityData>();
        collider = GetComponent<CapsuleCollider2D>();
        cargoPos = cargo.transform.position;
    }
    private void Start()
    {
        SetBT();
        root.Evaluate();
    }
    private void FixedUpdate()
    {
        root.Evaluate();
    }
    #endregion
    #region Coroutine
    IEnumerator GatherTimer()
    {
        yield return new WaitForSeconds(gatherTime);
        Debug.Log("Gather Finish");
        // �۾� �Ϸ� ����?
        entityData.isHolding = true;
        entityData.holdValue = entityData.gatherValue;

        //FindCargo(); //����� ��� �Ҵ�
        ChangeState(State.Move);
    }
    IEnumerator WallBreakTimer()
    {
        yield return new WaitForSeconds(wallBreakTime);
        Debug.Log("Break Finish");
        Wall node = (Wall)targetNode;
        if (node.GetResource() != null)
        {
            HexaMapNode resNode = MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "ResourceNode", true);
            MapManager.Map.ResourceFactory.SetResource(node, resNode as ResourceNode2);
        }
        else
        {
            MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "Path", true);
        }
        targetNode.SetIsWorked(false);
        ChangeState(State.Idle);
        currentTask = TaskType.None;
    }
    #endregion
    #endregion
    #region BT
    #region BTAction
    BTNodeState Store()
    {
        entityData.isHolding = false;
        Debug.Log("Stored");
        // �ڿ� ���� �߰�
        return BTNodeState.Success;
    }
    BTNodeState GatherResource()
    {
        if (currentTask == TaskType.Gather && state != State.Gather)   // ���� ���� ��
        {
            ChangeState(State.Gather);
            // ä�� ���ð�, �ִϸ��̼�
            StartCoroutine(GatherTimer());
        }
        else if (currentTask == TaskType.Gather && state != State.Gather)   // �Ǽ� ���� ��
        {
            return BTNodeState.Success;
        }
        return BTNodeState.Running;
    }
    BTNodeState Build()
    {
        if (currentTask == TaskType.Build && state != State.Build)   // ���� ���� ��
        {
            ChangeState(State.Build);
            // �Ǽ� �ڿ� �Ҹ� �� ���ð�, �ִϸ��̼�
            //StartCoroutine(BuildTimer());
            StartBuild();
        }
        else if (currentTask == TaskType.Build && state != State.Build)   // �Ǽ� ���� ��
        {
            return BTNodeState.Success;
        }
        return BTNodeState.Running;
    }
    #endregion
    #region BTCondition
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
}
