using System.Collections;
using System;
using UnityEngine;
using System.Linq;

public class Worker : Ant
{
    #region Attribute
    [SerializeField]
    GameObject cargo;

    Vector2 cargoPos;

    BuildingType buildingType;
    BaseResource resourceNode;
    public float wallBreakTime = 4f;    //임시 벽 파괴 시간
    public float gatherTime = 4f;    //임시 채집시간
    #endregion
    #region Function
    #region Public
    public void GetTask(HexaMapNode _targetNode, TaskType type)
    {
        Debug.Log("Task Confirmed");
        currentTask = type;
        targetNode = _targetNode;
        if (type == TaskType.Build)
        {
            RequestPath(_targetNode, true);
            buildingType = BuildingType.None;
        }
        else if (type == TaskType.Gather)
        {
            resourceNode = _targetNode.GetResource();
            entityData.resourceType = resourceNode.GetResourceType();
            RequestPath(_targetNode, false);
        }
        else
        {
            RequestPath(_targetNode, false);
        }
        pathIndex = path.Count - 1;
        targetPos = targetNode.GetWorldPos();
        currentTargetPos = path[pathIndex];
        targetGridPos = targetNode.GetGridPos();
    }
    public void GetTask(HexaMapNode _targetNode, TaskType type, BuildingType _buildingType)
    {
        Debug.Log("Task Confirmed");

        currentTask = type;
        targetNode = _targetNode;
        if (type == TaskType.Build)
            buildingType = _buildingType;

        RequestPath(_targetNode, false);
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
        BTSequence idleSequence = new BTSequence();

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
        root.AddChild(idleSequence);

        eatSequence.AddChild(kcalLow);
        //eatSequence.AddChild(moveAction);
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

        idleSequence.AddChild(idleAction);
        idleSequence.AddChild(moveAction);
    }
    void FindCargo(ResourceType resourceType)
    {
        int resourceLayer;    //해당 자원 레이어
        switch (resourceType)
        {
            case ResourceType.LEAF:
                resourceLayer = 1 << LayerMask.NameToLayer("LeafSaver");
                break;
            case ResourceType.WOOD:
                resourceLayer = 1 << LayerMask.NameToLayer("WoodSaver");
                break;
            case ResourceType.LIQUID_FOOD:
                resourceLayer = 1 << LayerMask.NameToLayer("LiquidSaver");
                break;
            case ResourceType.SOLID_FOOD:
                resourceLayer = 1 << LayerMask.NameToLayer("SolidSaver");
                break;
            default:
                resourceLayer = 0;
                break;
        }
        resourceLayer += 1 << LayerMask.NameToLayer("CombineSaver");

        GameObject obj = null;

        var hits = Physics2D.CircleCastAll(nodePos, Mathf.Infinity, Vector2.zero, Mathf.Infinity, resourceLayer);

        foreach (var hit in hits.OrderBy(distance => Vector2.Distance(nodePos, distance.point)))
        {
            obj = hit.collider.gameObject;
            Debug.Log("Found");
            break;
        }

        if (obj == null)
        {
            Debug.Log("Can't Find");
        }

        cargo = obj;
        cargoPos = cargo.transform.position;
    }
    void Gather()
    {
        Debug.Log("Finsh Gathering");
        int remain = resourceNode.GetCurrentAmount();
        if (remain <= entityData.gatherValue)   //자원 노드 고갈
        {
            // 노드 고갈
            entityData.holdValue = remain;
        }
        else
        {
            resourceNode.Extraction(entityData.gatherValue);
        }
        entityData.isHolding = true;

        FindCargo(entityData.resourceType);
        targetNode = MapManager.Map.UnderGrid.GetNode(cargoPos);
        RequestPath(targetNode, false);
        ChangeState(State.Idle);
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
    void BreakWall()
    {
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
    #region Unity
    private void Start()
    {
        SetBT();
    }
    private void FixedUpdate()
    {
        root.Evaluate();
    }
    #endregion
    #region Coroutine
    //IEnumerator GatherTimer()
    //{
    //    yield return new WaitForSeconds(gatherTime);
    //    Debug.Log("Gather Finish");
    //    // 작업 완료 전달?
    //    entityData.isHolding = true;
    //    entityData.holdValue = entityData.gatherValue;

    //    //FindCargo(); //저장소 경로 할당
    //    ChangeState(State.Move);
    //    isCorutineRunning = false;
    //}
    //IEnumerator WallBreakTimer()
    //{
    //    yield return new WaitForSeconds(wallBreakTime);
    //    Debug.Log("Break Finish");

    //    Wall node = (Wall)targetNode;
    //    if (node.GetResource() != null)
    //    {
    //        HexaMapNode resNode = MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "ResourceNode", true);
    //        MapManager.Map.ResourceFactory.SetResource(node, resNode as ResourceNode2);
    //    }
    //    else
    //    {
    //        MapManager.Map.UnderGrid.SwapNode(targetGridPos.x, targetGridPos.y, "Path", true);
    //    }
    //    targetNode.SetIsWorked(false);
    //    ChangeState(State.Idle);
    //    currentTask = TaskType.None;
    //}
    #endregion
    #endregion
    #region BT
    #region BTAction
    BTNodeState Store()
    {
        Debug.Log("Stored");
        switch(entityData.resourceType)
        {
            case ResourceType.LEAF:
                Managers.Resource.AddLeaf(entityData.holdValue);
                break;
            case ResourceType.WOOD:
                Managers.Resource.AddWood(entityData.holdValue);
                break;
            case ResourceType.LIQUID_FOOD:
                Managers.Resource.AddLiquidFood(entityData.holdValue);
                break;
            case ResourceType.SOLID_FOOD:
                Managers.Resource.AddSolidFood(entityData.holdValue);
                break;
        }
        entityData.isHolding = false;
        entityData.holdValue = 0;
        return BTNodeState.Success;
    }
    BTNodeState GatherResource()
    {
        if (currentTask == TaskType.Gather && state != State.Gather)   // 최초 진입 시
        {
            ChangeState(State.Gather);
        }
        if (currentTask == TaskType.Gather)
        { 
            if (currentTimer < gatherTime)
            {
                currentTimer += Time.deltaTime;
                return BTNodeState.Running;
            }
            else
            {
                currentTimer = 0;
                Gather();
                return BTNodeState.Success;
            }
        }
        else if (currentTask == TaskType.Gather && state != State.Gather)
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
            currentTimer = 0;
        }
        if (currentTask == TaskType.Build)
        {
            if (buildingType == BuildingType.None)  //벽 파괴 시
            {
                if (currentTimer < wallBreakTime)
                {
                    currentTimer += Time.deltaTime;
                    return BTNodeState.Running;
                }
                else
                {
                    currentTimer = 0;
                    Debug.Log("Start Breaking Wall");
                    BreakWall();
                    return BTNodeState.Success;
                }
            }
            else  // 건물 건설 시
            {
                Debug.Log("Start Building");
                MapManager.Map.BuildingFactory.Build((Path)targetNode, buildingType, OnBuildFinish);
                return BTNodeState.Running;
            }
        }
        else if (currentTask == TaskType.Build && state != State.Build)   // 건설 종료 후
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
