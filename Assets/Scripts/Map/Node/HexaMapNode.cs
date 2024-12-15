using UnityEngine;

public class HexaMapNode
{
    #region Attribute
    bool buildable;
    bool walkable;
    bool breakable;
    bool isWorked = false;
    protected BaseBuilding building;
    TileType type;

    Vector2Int gridPos;
    Vector3 worldPos;
    Vector3Int cellPos;

    HexaMapNode parent;
    int fcost;
    int hcost;
    #endregion

    #region Getter & Setter
    public BaseBuilding GetBuilding()
    {
        return building;
    }
    public virtual void SetBuilding(BaseBuilding building)
    {
        if (GetBuildable())
        {
            this.building = building;
            SetBuildable(false);
            building.EventStart();
        }
    }
    public void SetGridPos(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }
    public Vector2Int GetGridPos()
    {
        return gridPos;
    }
    public void SetWorldPos(Vector3 worldPos)
    {
        this.worldPos = worldPos;
    }
    public Vector3 GetWorldPos()
    {
        return worldPos;
    }
    public void SetCellPos(Vector3Int cellPos)
    {
        this.cellPos = cellPos;
    }
    public Vector3Int GetCellPos()
    {
        return cellPos;
    }
    public void SetFcost(int fcost)
    {
        this.fcost = fcost;
    }
    public int GetFcost()
    {
        return fcost;
    }
    public void SetHcost(int hcost)
    {
        this.hcost = hcost;
    }
    public int GetHcost()
    {
        return hcost;
    }
    public void SetBuildable(bool buildable)
    {
        this.buildable = buildable;
    }
    public bool GetBuildable()
    {
        return buildable;
    }
    public void SetBreakable(bool breakable)
    {
        this.breakable = breakable;
    }
    public bool GetBreakable()
    {
        return breakable;
    }
    public void SetWalkable(bool walkable)
    {
        this.walkable = walkable;
    }
    public bool GetWalkable()
    {
        return walkable;
    }
    public void SetParent(HexaMapNode parent)
    {
        this.parent = parent;
    }
    public HexaMapNode GetParent()
    {
        return parent;
    }
    public void SetTileType(TileType tileType)
    {
        type = tileType;
    }
    public TileType GetTileType()
    {
        return type;
    }
    public bool GetIsWorked()
    {
        return isWorked;
    }
    public void SetIsWorked(bool isWorked)
    {
        this.isWorked = isWorked;
    }
    #endregion

    #region Fuction
    public virtual void Demolition()
    {
        SetBuildable(true);
        building.EventStop();
        this.building = null;
    }
    public void SetNodePosition(HexaMapNode node)
    {
        SetGridPos(node.GetGridPos());
        SetWorldPos(node.GetWorldPos());
        SetCellPos(node.GetCellPos());
    }
    public virtual void Start()
    {

    }
    #endregion
}