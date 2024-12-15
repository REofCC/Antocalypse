public class ResourceNode2 : Path
{
    #region Attribute
    #endregion

    #region Getter & Setter
    #endregion

    public override void Start()
    {
        SetBreakable(false);
        SetWalkable(true);
        SetBuildable(false);
        SetTileType(TileType.ResourceNode);
    }
}
