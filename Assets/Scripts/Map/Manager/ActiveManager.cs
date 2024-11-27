using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveManager : MonoBehaviour
{
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
    #endregion

    #region Function
    private HexaMapNode ClickTile(Vector3 pos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z * -1));
        Debug.Log(mouseWorldPos);

        Vector2Int gridPos = MapManager.Map.PositionCalc.CalcGridPos(mouseWorldPos);
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
        if (node == null || !MapManager.Map.UpGrid.IsBreakable(node)) return;

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
    public void BuildBuilding()
    {
        if (node == null)
        {
            Debug.Log("current node is null");
            return;
        }
        MapManager.Map.BuildingFactory.Build((RoomNode)node, "BaseBuilding");
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
        List<Vector3> route = MapManager.Map.PathFinder.PathFinding(start, GetCurrentNode());
        Debug.Log(route);
    }

    public void MakeMap()
    {
        MapManager.Map.MapMaker.MapMaking();
    }
    #endregion

    #region Unity Function

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
