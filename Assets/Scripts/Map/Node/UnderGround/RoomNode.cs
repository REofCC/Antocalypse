public class RoomNode : HexaMapNode
{
    #region Attribute
    RoomCenter center;
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
    
    #endregion

    #region Function
    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(true);
        SetWalkable(true);
        SetTileType(TileType.RoomNode);
    }
    #endregion
}