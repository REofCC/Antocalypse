using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexaGrid : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    CellPositionCalc cellPositionCalc;
    [SerializeField]
    Tilemap tilemap;
    NodeFactory tileFactory;

    HexaMapNode[,] hexgrid;

    DoorNode door;

    int mapSizeX;
    int mapSizeY;
    #endregion
    #region Direction for Get Node Circlular Rotation
    //Unity Cell System = odd-r Horizontal Layout
    readonly int[] ringNodeNum = { 6, 12, 18 };

    readonly int[] roomNodeNum = { 6, 18 };

    int[][] oddDirX;
    int[][] evenDirX;
    int[][] DirY;

    readonly int[] oddDirX1 = { 0, 1, 1, 1, 0, -1 };
    readonly int[] oddDirX2 = { -1, 0, 1, 2, 2, 2, 1, 0, -1, -1, -2, -1 };
    readonly int[] oddDirX3 = { -1, 0, 1, 2, 2, 3, 3, 3, 2, 2, 1, 0, -1, -2, -2, -3, -2, -2 };

    readonly int[] evenDirX1 = { -1, 0, 1, 0, -1, -1 };
    readonly int[] evenDirX2 = { -1, 0, 1, 1, 2, 1, 1, 0, -1, -2, -2, -2 };
    readonly int[] evenDirX3 = { -2, -1, 0, 1, 2, 2, 3, 2, 2, 1, 0, -1, -2, -2, -3, -3, -3, -2 };

    readonly int[] DirY1 = { -1, -1, 0, 1, 1, 0 };
    readonly int[] DirY2 = { -2, -2, -2, -1, 0, 1, 2, 2, 2, 1, 0, -1 };
    readonly int[] DirY3 = { -3, -3, -3, -3, -2, -1, 0, 1, 2, 3, 3, 3, 3, 2, 1, 0, -1, -2 };

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
    public HexaMapNode GetNode(int x, int y) //need gridPos
    {
        return hexgrid[x, y];
    }
    public HexaMapNode GetNode(GameObject gameObject)
    {
        Vector2Int gridPos = cellPositionCalc.CalcGridPos(gameObject.transform.position);

        return hexgrid[gridPos.x, gridPos.y];
    }
    public HexaMapNode GetNode(Vector3 pos) //need worldPosition
    {
        Vector2Int gridPos = cellPositionCalc.CalcGridPos(pos);

        return hexgrid[gridPos.x, gridPos.y];
    }
    public CellPositionCalc GetCellPosCalc()
    {
        return cellPositionCalc;
    }
    public HexaMapNode[,] GetGrid()
    {
        return hexgrid;
    }
    public void SetDirection()
    {
        evenDirX = new int[3][];
        evenDirX[0] = new int[evenDirX1.Length];
        evenDirX[1] = new int[evenDirX2.Length];
        evenDirX[2] = new int[evenDirX3.Length];

        oddDirX = new int[3][];
        oddDirX[0] = new int[oddDirX1.Length];
        oddDirX[1] = new int[oddDirX2.Length];
        oddDirX[2] = new int[oddDirX3.Length];

        DirY = new int[3][];
        DirY[0] = new int[DirY1.Length];
        DirY[1] = new int[DirY2.Length];
        DirY[2] = new int[DirY3.Length];

        for (int i = 0; i < evenDirX1.Length; i++)
        {
            evenDirX[0][i] = evenDirX1[i];
            oddDirX[0][i] = oddDirX1[i];
            DirY[0][i] = DirY1[i];
        }

        for (int i = 0; i < evenDirX2.Length; i++)
        {
            evenDirX[1][i] = evenDirX2[i];
            oddDirX[1][i] = oddDirX2[i];
            DirY[1][i] = DirY2[i];
        }

        for (int i = 0; i < evenDirX3.Length; i++)
        {
            evenDirX[2][i] = evenDirX3[i];
            oddDirX[2][i] = oddDirX3[i];
            DirY[2][i] = DirY3[i];
        }

    }

    public int[][] GetDirectionOddX()
    {
        return oddDirX;
    }
    public int[][] GetDirectionEvenX()
    {
        return evenDirX;
    }
    public int[][] GetDirectionY()
    {
        return DirY;
    }
    public DoorNode GetDoorPos()
    {
        return door;
    }
    public void SetDoorPos(DoorNode door)
    {
        this.door = door;
    }
    #endregion

    #region Function
    #region Check
    private bool IsNodeExist(int x, int y)
    {
        if (x < 0 || y < 0 || x > GetMapSizeX() || y > GetMapSizeY())
        {
            return false;
        }
        if (GetNode(x, y) != null)
        {
            return true;
        }
        return false;
    }
    private bool CheckDoorConnect(HexaMapNode node)
    {
        Vector2Int gridPos = node.GetGridPos();
        List<HexaMapNode> list = GetNeighborNode(gridPos.x, gridPos.y);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetTileType() == TileType.DoorNode)
                return true;
        }
        return false;
    }
    #endregion
    #region Grid
    private void CalcMapSize()
    {
        BoundsInt bound = tilemap.cellBounds;
        Vector2Int offset = cellPositionCalc.CalcOffset(tilemap);
        SetMapSizeX(offset.x + bound.xMax + 1);
        SetMapSizeY(offset.y + bound.yMax + 1);
    }
    public void MakeGrid()
    {
        CalcMapSize();
        hexgrid = new HexaMapNode[GetMapSizeX(), GetMapSizeY()];
        Vector2Int offset = cellPositionCalc.GetOffset();
        int z = tilemap.cellBounds.zMax-1;
        for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
            {
                Vector3Int pos = new(x, y, z);
                if (tilemap.GetTile(pos) != null)
                {
                    string name = tilemap.GetTile(pos).name;
                    HexaMapNode node = tileFactory.GetNode(name);
                    node.Start();
                    node.SetGridPos(new Vector2Int(x + offset.x, y + offset.y));
                    node.SetCellPos(pos);
                    node.SetWorldPos(tilemap.CellToWorld(pos));
                    SetNode(x + offset.x, y + offset.y, node);
                }
            }
        }
    }
    #endregion
    #region Neighbor
    public List<HexaMapNode> GetNeighborNode(int x, int y, int phase = 1) // max phase = 3 min phase = 1
    {
        List<HexaMapNode> neighbors = new();
        for (int p = 0; p < phase; p++)
        {
            List<HexaMapNode> list = GetNeighborRingNode(x, y, p);
            for (int i = 0; i < list.Count; i++)
            {
                neighbors.Add(list[i]);
            }
        }

        return neighbors;
    }
    public List<HexaMapNode> GetNeighborWalkableNode(int x, int y)
    {
        List<HexaMapNode> result = new();
        List<HexaMapNode> neighbors = GetNeighborNode(x,y,1);
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i].GetWalkable())
            {
                result.Add(neighbors[i]);
            }
        }

        return result;
    }
    public List<HexaMapNode> GetNeighborRingNode(int x, int y, int phase) // max phase = 0 min phase = 2
    {
        int idxX, idxY;
        List<HexaMapNode> neighbors = new();
        if (y % 2 == 0)
        {
            for (int i = 0; i < ringNodeNum[phase]; i++)
            {
                idxX = x + evenDirX[phase][i];
                idxY = y + DirY[phase][i];
                if (IsNodeExist(idxX, idxY))
                {
                    neighbors.Add(GetNode(idxX, idxY));
                }
            }
        }
        else
        {
            for (int i = 0; i < ringNodeNum[phase]; i++)
            {
                idxX = x + oddDirX[phase][i];
                idxY = y + DirY[phase][i];
                if (IsNodeExist(idxX, idxY))
                {
                    neighbors.Add(GetNode(idxX, idxY));
                }
            }
        }
        return neighbors;
    }
    #endregion
    #region TileSwap
    public HexaMapNode SwapNode(int x, int y, string tile, bool is_pass)
    {
        HexaMapNode prevNode = GetNode(x, y);
        HexaMapNode node = tileFactory.GetNode(tile);
        node.Start();
        node.SetNodePosition(prevNode);
        SetNode(x, y, node);
        tilemap.SetTile(node.GetCellPos(), tileFactory.GetTile(((int)node.GetTileType())));
        if (is_pass && !door.IsConnected())
        {
            door.SetConnect(CheckDoorConnect(node));
        }
        if (is_pass)
        {
            MapManager.Map.BlackMask.EraseNeighborNode(x, y);
        }
        return node;
    }
    #endregion
    #endregion
    #region Unity Function
    public void OnAwake(Tilemap tilemap, CellPositionCalc calc)
    {
        SetDirection();
        cellPositionCalc = calc;

        tileFactory = MapManager.Map.NodeFactory;
        this.tilemap = tilemap;
    }
    #endregion
}
