public class Path : HexaMapNode
{
    #region
    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(false);
        SetWalkable(true);
        SetTileType(TileType.Path);
    }
    #endregion
}