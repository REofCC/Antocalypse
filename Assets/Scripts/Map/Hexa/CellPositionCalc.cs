using UnityEngine;
using UnityEngine.Tilemaps;

public class CellPositionCalc
{
    #region Attribute
    Vector2Int offset;
    Tilemap tilemap;
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
    public void SetTileMap(Tilemap tilemap)
    {
        this.tilemap = tilemap;
    }
    public Tilemap GetTileMap()
    {
        return tilemap;
    }
    #endregion

    #region Function
    public Vector2Int CalcOffset(Tilemap tilemap)
    {
        BoundsInt bound = tilemap.cellBounds;
        SetOffset(new Vector2Int(Mathf.Abs(bound.xMin), Mathf.Abs(bound.yMin)));
        return GetOffset();
    }
    public Vector2 CalcWorldPos(HexaMapNode mapNode)
    {
        return tilemap.CellToWorld(new Vector3Int(mapNode.GetCellPos().x - offset.x, mapNode.GetCellPos().y - offset.y, 0));
    }
    public Vector3Int CalcCellPos(Vector2 pos)
    {
        return tilemap.WorldToCell(pos);
    }
    #endregion
}
