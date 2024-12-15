using UnityEngine;
using UnityEngine.EventSystems;

public class StateManager : MonoBehaviour
{
    #region Attribute
    HexaMapNode node;
    BaseBuilding building;
    BaseResource resource;
    Event travelEvent;
    bool isGround = false;
    #endregion
    #region Setter
    private void SetCurrentNode(HexaMapNode node)
    {
        this.node = node;
    }
    private void SetBuilding(BaseBuilding building)
    {
        this.building = building;
    }
    private void SetEvent(Event travelEvent)
    {
        this.travelEvent = travelEvent;
    }
    private void SetResource(BaseResource res)
    {
        this.resource = res;
    }
    private void SetIsGround(bool isGround)
    {
        this.isGround = isGround;
    }
    #endregion
    #region Getter
    public HexaMapNode GetCurrentNode() //return click node
    {
        return node;
    }
    public BaseResource GetResource() //return click resource
    {
        return resource;
    }
    public BaseBuilding GetBuilding() //return click building
    {
        return this.building;
    }
    public Event GetEvent() //return click event
    {
        return travelEvent;
    }
    public bool IsGround()
    {
        return isGround;
    }
    
    #endregion
    #region Function
    private bool CheckGroundMask(HexaMapNode node)
    {
        Vector3Int pos = node.GetCellPos();
            return MapManager.Map.UpBlackMask.CheckMask(pos);
    }
    private HexaMapNode ClickUnderGrid(Vector3 pos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z * -1));
        Vector2Int gridPos = MapManager.Map.UnderPosCalc.CalcGridPos(mouseWorldPos);
        return MapManager.Map.UnderGrid.GetNode(mouseWorldPos);
    }
    private HexaMapNode ClickGroundGrid(Vector3 pos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z * -1));
        Vector2Int Pos = MapManager.Map.UpPosCalc.CalcGridPos(mouseWorldPos);

        return MapManager.Map.UpGrid.GetNode(mouseWorldPos);
    }
    private void ClickOnUnderGrid(Vector3 pos)
    {
        HexaMapNode node = ClickUnderGrid(pos);
        if (node.GetIsWorked()) return;
        BaseResource res = node.GetResource();
        BaseBuilding building = node.GetBuilding();

        SetCurrentNode(node);
        SetResource(res);
        SetBuilding(building);
    }
    private void ClickOnGroundGrid(Vector3 pos)
    {
        HexaMapNode node = ClickUnderGrid(pos);
        if (CheckGroundMask(node) || node.GetIsWorked()) return;
        BaseResource res = node.GetResource();
        BaseBuilding building = node.GetBuilding();
        TravelNode travelNode = (TravelNode)node;

        SetEvent(travelNode.GetEvent());
        SetCurrentNode(node);
        SetResource(res);
        SetBuilding(building);
    }
    public void ChangeMod()
    {
        Vector3 Pos = new Vector3(100, 0, 0);
        if (isGround)
        {
            isGround = false;
            Camera.main.transform.position -= Pos;
        }
        else
        {
            isGround = true;
            Camera.main.transform.position += Pos;
        }
    }
    #endregion
    #region UnityFunction
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (isGround)
                {
                    ClickOnGroundGrid(Input.mousePosition);
                }
                else
                {
                    ClickOnUnderGrid(Input.mousePosition);
                }
            }
        }
    }
    #endregion
}
