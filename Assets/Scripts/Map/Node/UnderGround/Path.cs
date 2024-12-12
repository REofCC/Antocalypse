public class Path : HexaMapNode
{
    protected BaseBuilding building;
    
    public BaseBuilding GetBuilding()
    {
        return building;
    }
    #region Function
    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(true);
        SetWalkable(true);
        SetTileType(TileType.Path);
    }
    public virtual void Demolition()
    {
        SetBuildable(true);
        building.EventStop();
        this.building = null;
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
    #endregion
}