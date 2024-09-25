using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class HexaGrid : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    CellPositionCalc cellPositionCalc;
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    HexaTileDict tileDict;

    HexaMapNode[,] hexgrid;
    bool[,] walkables;

    int mapSizeX;
    int mapSizeY;
    #endregion
    #region Direction
    int[] oddDirX = {0,1,1,1,0,-1 };
    int[] evenDirX = {-1,0,1,0,-1,-1 };
    int[] ypos = {-1,-1,0,1,1,0 };
    #endregion
    #region Getter & Setter
    public void SetMapSizeX(int mapSizeX)
    {
        this.mapSizeX = mapSizeX;
    }
    public int GetMapSizeX()
    {
        return mapSizeX;
    }
    public void SetMapSizeY(int mapSizeY)
    {
        this.mapSizeY = mapSizeY;
    }
    public int GetMapSizeY()
    {
        return mapSizeY;
    }
    public void SetNode(int x, int y, HexaMapNode node)
    {
        hexgrid[x, y] = node;
    }
    public HexaMapNode GetNode(int x, int y)
    {
        return hexgrid[x, y];
    }
    #endregion

    #region Function
    private void CalcMapSize()
    {
        BoundsInt bound = tilemap.cellBounds;
        Vector2Int offset = cellPositionCalc.CalcOffset(tilemap);
        SetMapSizeX(offset.x + bound.xMax + 1);
        SetMapSizeY(offset.y + bound.yMax + 1);
    }
    private void MakeGrid()
    {
        CalcMapSize();
        hexgrid = new HexaMapNode[GetMapSizeX(), GetMapSizeY()];
        Vector2Int offset = cellPositionCalc.CalcOffset(tilemap);
        for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
            {
                string name = tilemap.GetTile(new Vector3Int(x, y,-10)).name;
                HexaMapNode node = tileDict.GetNode(name);
                SetNode(x + offset.x, y + offset.y, node);
            }
        }
    }
    #endregion

    #region Unity Function
    private void Start()
    {
        cellPositionCalc = new CellPositionCalc();
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();

        MakeGrid();
    }
    #endregion
}