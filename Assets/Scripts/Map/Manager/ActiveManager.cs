using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveManager : MonoBehaviour
{
    #region SingleTon
    private static ActiveManager activeManager;
    public static ActiveManager Active { get { return activeManager; } }
    #endregion

    #region Attribute
    HexaMapNode node;
    BaseBuilding building;
    #endregion

    #region Getter & Setter
    public HexaMapNode GetCurrentNode()
    {
        return node;
    }

    public void SetCurrentNode(HexaMapNode node)
    {
        this.node = node;
    }

    public void SetBuilding(BaseBuilding building)
    {
        this.building = building;
    }

    public Vector3 GetStartWorlePos()
    {
        HexaMapNode startNode = MapManager.Map.MapMaker.GetStartPos();
        return startNode.GetWorldPos();
    }
    #endregion

    #region Function
    private HexaMapNode ClickTile(Vector3 pos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z * -1));
        Debug.Log(mouseWorldPos);

        Vector2Int gridPos = MapManager.Map.UnderPosCalc.CalcGridPos(mouseWorldPos);
        return MapManager.Map.UnderGrid.GetNode(mouseWorldPos);
    }
    private void ClickBuilding()
    {
        if (node.GetTileType() == TileType.RoomNode || node.GetTileType() == TileType.RoomCenter)
        {
            RoomNode room = (RoomNode)node;
            if (room.GetBuilding() != null)
            {
                SetBuilding(room.GetBuilding());
                return;
            }
        }
        building = null;
    }
    public void BreakTile()
    {
        if (node == null || !node.GetBreakable()) return;

        Vector2Int gridPos = node.GetGridPos();
        node.SetIsWorked(true);
        Wall Node = (Wall)node;
        if (Node.GetResource() != null)
        {
            HexaMapNode resNode = MapManager.Map.UnderGrid.SwapNode(gridPos.x, gridPos.y, "ResourceNode", true);
            MapManager.Map.ResourceFactory.SetResource(Node, resNode as ResourceNode2);
        }
        else
        {
            MapManager.Map.UnderGrid.SwapNode(gridPos.x, gridPos.y, "Path", true);
        }
        node.SetIsWorked(false); //When Complete Work Must be false;
    }
    public void MakeRoom()
    {
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }
        MapManager.Map.RoomFactory.MakeRoom(node);
    }
    public void ExpandRoom()
    {
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }

        MapManager.Map.RoomFactory.ExpandRoom((RoomCenter)node);
    }
    public void BuildBuilding(BuildingType type)
    {
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }
        MapManager.Map.BuildingFactory.Build((RoomNode)node, type);
    }
    public void UpgradeBuilding()
    {
        if(building == null)
        {
            Debug.Log("current building is null");
            return;
        }
        MapManager.Map.BuildingFactory.Upgrade(building);
    }
    public void DemolitionBuilding()
    {
        if (building == null)
        {
            Debug.Log("current building is null");
            return;
        }
        MapManager.Map.BuildingFactory.Demolition(building.gameObject);
    }
    public List<Vector3> PathFind()
    {
        HexaMapNode start = MapManager.Map.UnderGrid.GetNode(15,15);
        List<Vector3> route = MapManager.Map.UnderPathFinder.PathFinding(start, GetCurrentNode());
        if(route != null)
        {
            Debug.Log(route);
            return route;
        }
        route = MapManager.Map.UnderPathFinder.ReachWallPathFinding(start, GetCurrentNode());
        Debug.Log(route);
        return route;
    }
    public HexaMapNode GetRandomWalkableNode(HexaMapNode node)
    {
        Vector2Int pos = node.GetGridPos();
        List<HexaMapNode> list = MapManager.Map.UnderGrid.GetNeighborWalkableNode(pos.x, pos.y);

        int idx = Random.Range(0, list.Count);

        return list[idx];
    }
    private bool CheckMask(HexaMapNode node)
    {
        Vector3Int pos = node.GetCellPos();
        return MapManager.Map.BlackMask.CheckMask(pos);
    }
    #endregion

    #region Unity Function
    private void Awake()
    {
        //[LSH: building-ui-integration] 인스턴스 초기화 추가
        if (activeManager == null)
        {
            activeManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HexaMapNode node = ClickTile(Input.mousePosition);
                if (CheckMask(node))
                {
                    if(!node.GetIsWorked())
                        SetCurrentNode(node);
                    ClickBuilding();
                    //if (building == null)
                    //{
                    //    Debug.Log(node);
                    //}
                    //else
                    //{
                    //    Debug.Log(building);
                    //}
                }
            }
        }
    }
}
#endregion
