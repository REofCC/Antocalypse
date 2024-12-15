using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{
    #region SingleTon
    private static ActiveManager activeManager;
    public static ActiveManager Active { get { return activeManager; } }
    #endregion

    #region Attribute
    StateManager state;
    #endregion

    #region Function
    public void BreakTile()
    {
        HexaMapNode node = state.GetCurrentNode();
        if (node == null || !node.GetBreakable()) return;

        Vector2Int gridPos = node.GetGridPos();
        node.SetIsWorked(true);
        Managers.Task.RequestTask(node, TaskType.Build);
        /*
        //Wall Node = (Wall)node;
        //if (Node.GetResource() != null)
        //{
        //    HexaMapNode resNode = MapManager.Map.UnderGrid.SwapNode(gridPos.x, gridPos.y, "ResourceNode", true);
        //    MapManager.Map.ResourceFactory.SetResource(Node, resNode as ResourceNode2);
        //}
        //else
        //{
        //    node.SetIsWorked(false); //When Complete Work Must be false;
        //    // ������ - TaskManager ���� �׽�Ʈ
        //    Managers.Task.RequestTask(node, TaskType.Build);
        //}
        */
        node.SetIsWorked(false);
    } //dig the ground
    public void BuildBuilding(BuildingType type)
    {
        HexaMapNode node = state.GetCurrentNode();
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }
        Managers.Task.RequestTask(node, TaskType.Build, type);
        MapManager.Map.BuildingFactory.Build((Path)node, type, result =>
        {
            if (!result) Debug.Log("");
        });
    } //build building
    public void DemolitionBuilding()
    {
        BaseBuilding building = state.GetBuilding();
        if (building == null)
        {
            Debug.Log("current building is null");
            return;
        }
        MapManager.Map.BuildingFactory.Demolition(building.gameObject);
    } //destroy building
    public List<Vector3> PathFindOnUnderGrid(GameObject go)
    {
        HexaMapNode start = MapManager.Map.UnderGrid.GetNode(go);
        List<Vector3> route = MapManager.Map.UnderPathFinder.PathFinding(start, state.GetCurrentNode());
        if (route != null)
        {
            Debug.Log(route);
            return route;
        }
        route = MapManager.Map.UnderPathFinder.ReachWallPathFinding(start, state.GetCurrentNode());
        Debug.Log(route);
        return route;
    } //path find under
    public List<Vector3> PathFindingOnGroundGrid()
    {
        List<Vector3> list = MapManager.Map.UpPathFinder.PathFindTravelNode(state.GetCurrentNode());
        MapManager.Map.TravelTrail.DrawLine(list);

        return list;
    } //path find ground
    public Vector3 GetRandomNeighborPos(GameObject go)
    {
        HexaMapNode node = MapManager.Map.UnderGrid.GetNode(go);
        node = MapManager.Map.UnderGrid.GetRandomWalkableNode(node);
        return node.GetWorldPos();
    } //get random neighbor node which can walk
    public void MoveToGroundMap(GameObject go)
    {
        Vector3 pos = MapManager.Map.UpGrid.GetDoorPos().GetWorldPos();
        go.transform.position = pos;
    }
    public void MoveToUnderMap(GameObject go)
    {
        Vector3 pos = MapManager.Map.UnderGrid.GetDoorPos().GetWorldPos();
        go.transform.position = pos;
    }
    // ������ - ��ü ���� �ڵ� �߰�
    public void SpwanEgg(AntType type)
    {
        Managers.SpawnManager.SpawnEgg(type);
        return;
    }
    // ������ ------
    #endregion
    #region Unity Function
    private void Awake()
    {
        //[LSH: building-ui-integration] �ν��Ͻ� �ʱ�ȭ �߰�
        if (activeManager == null)
        {
            activeManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        state = MapManager.Map.State;
    }
}
#endregion
