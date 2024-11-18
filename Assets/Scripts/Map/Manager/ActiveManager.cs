using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveManager : MonoBehaviour
{
    #region Attribute
    HexaGrid grid;
    BuildingFactory builder;
    RoomFactory roombuilder;
    HexaMapNode node;
    HexaPathFinder finder;
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
    #endregion

    #region Function
    private HexaMapNode ClickTile(Vector3 pos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z * -1));
        Debug.Log(mouseWorldPos);

        Vector2Int gridPos = grid.GetCellPosCalc().CalcGridPos(mouseWorldPos);
        Debug.Log(gridPos);
        return grid.GetNode(gridPos.x, gridPos.y);
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
        if (node == null || !grid.IsBreakable(node)) return;

        Vector2Int gridPos = node.GetGridPos();
        grid.SwapNode(gridPos.x, gridPos.y, "Path");
    }
    public void MakeRoom()
    {
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }
        roombuilder.MakeRoom(node);
    }
    public void ExpandRoom()
    {
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }

        roombuilder.ExpandRoom((RoomCenter)node);
    }

    public void BuildBuilding()
    {
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }
        builder.Build((RoomNode)node, "BaseBuilding");
    }
    public void UpgradeBuilding()
    {
        if(building == null)
        {
            Debug.Log("current building is null");
            return;
        }
        builder.Upgrade(building);
    }
    public void DemolitionBuilding()
    {
        if (building == null)
        {
            Debug.Log("current building is null");
            return;
        }
        builder.Demolition(building.gameObject);
    }

    public void PathFind()
    {
        HexaMapNode start = grid.GetNode(15,15);
        List<Vector3> route = finder.PathFinding(start, GetCurrentNode());
        Debug.Log(route);
    }
    #endregion

    #region Unity Function
    private void Start()
    {
        grid = GameObject.Find("MapTool").GetComponent<HexaGrid>();
        builder = GameObject.Find("MapTool").transform.GetChild(0).GetComponent<BuildingFactory>();
        finder = GameObject.Find("MapTool").GetComponent<HexaPathFinder>();
        roombuilder = GameObject.Find("MapTool").transform.GetChild(0).GetComponent<RoomFactory>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SetCurrentNode(ClickTile(Input.mousePosition));
                ClickBuilding();
                if(building == null)
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
#endregion
