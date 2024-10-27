public class RoomNode : HexaMapNode
{
    #region Attribute
    RoomCenter center;
    protected BaseBuilding building;
    #endregion

    #region Getter & Setter
    public void SetCenter(RoomCenter center)
    {
        this.center = center;
    }
    public RoomCenter GetCenter() 
    {
        return center;
    }
    public virtual void SetBuilding(BaseBuilding building)
    {
        if (GetBuildable())
        {
            this.building = building;
            SetBuildable(false);
            center.AddBuildings(building);
        }
    }
    public BaseBuilding GetBuilding()
    {
        return building;
    }
    
    #endregion

    #region Function
    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(true);
        SetWalkable(true);
        SetTileType(TileType.RoomNode);
    }
    public virtual void Demolition()
    {
        SetBuildable(true);
        center.RemoveBuildings(building);
        this.building = null;
    }
    #endregion
}