using UnityEngine;

public class TraveledNode : HexaMapNode
{
    #region Attribute
    #endregion

    #region Getter & Setter
    #endregion

    #region Function

    public override void Start()
    {
        SetBreakable(false);
        SetBuildable(false);
        SetWalkable(true);
        SetTileType(TileType.TraveledNode);
    }
    #endregion
}
