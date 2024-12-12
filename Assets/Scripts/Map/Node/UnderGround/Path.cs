public class Path : HexaMapNode
{
    #region Function
    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(true);
        SetWalkable(true);
        SetTileType(TileType.Path);
    }
    #endregion
}