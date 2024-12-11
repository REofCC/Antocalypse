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
    }

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
        MapManager.Map.UnderGrid.SwapNode(gridPos.x, gridPos.y, "Path", true);
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
    public void PathFind()
    {
        HexaMapNode start = MapManager.Map.UnderGrid.GetNode(15,15);
        List<Vector3> route = MapManager.Map.UnderPathFinder.PathFinding(start, GetCurrentNode());
        Debug.Log(route);
    }
    public void PathFindWall()
    {
        HexaMapNode start = MapManager.Map.UnderGrid.GetNode(15, 15);
        List<Vector3> route = MapManager.Map.UnderPathFinder.ReachWallPathFinding(start, GetCurrentNode());
        Debug.Log(route);
    }
    public void MakeMap()
    {
        MapManager.Map.MapMaker.MapMaking();
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
    // ������ - ��ü ���� �ڵ� �߰�
    public void SpwanWorker()
    {
        Managers.SpawnManager.SpawnEgg(AntType.Worker);
        return;
    }
    public void SpwanScout()
    {
        Managers.SpawnManager.SpawnEgg(AntType.Scout);
        return;
    }
    public void SpwanSoldier()
    {
        Managers.SpawnManager.SpawnEgg(AntType.Soldier);
        return;
    }
    // ������ ------
    #endregion

    #region Unity Function

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HexaMapNode node = ClickTile(Input.mousePosition);
                if (CheckMask(node))
                {
                    SetCurrentNode(node);
                    ClickBuilding();
                    if (building == null)
                    {
                        Debug.Log(node);
                    }
                    else
                    {
                        Debug.Log(building);
                    }
                }
            }
        }
    }
}
#endregion
