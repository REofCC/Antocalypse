using UnityEngine;
using UnityEngine.Tilemaps;

public class CellPositionCalc
{
    #region Attribute
    Vector2Int offset;
    #endregion

    #region Getter & Setter
    public Vector2Int GetOffset()
    {
        return offset;
    }
    public void SetOffset(Vector2Int offset)
    {
        this.offset = offset;
    }
    #endregion

    #region Function
    public Vector2Int CalcOffset(Tilemap tilemap)
    {
        BoundsInt bound = tilemap.cellBounds;
        SetOffset(new Vector2Int(Mathf.Abs(bound.xMin), Mathf.Abs(bound.yMin)));
        return GetOffset();
    }
    #endregion
}
