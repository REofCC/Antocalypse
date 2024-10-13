using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HexaGrid : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    CellPositionCalc cellPositionCalc;
    [SerializeField]
    Tilemap tilemap;
    NodeFactory tileFactory;

    HexaMapNode[,] hexgrid;
    bool[,] walkables;

    int mapSizeX;
    int mapSizeY;
    #endregion
    #region Direction for Get Node Circlular Rotation
    //Unity Cell System = Even-r Horizontal Layout
    int[] ringNodeNum = { 6, 12, 18 };

    int[] roomNodeNum = {6, 18};

    int[][] oddDirX;
    int[][] evenDirX;
    int[][] DirY;

    int[] evenDirX1 = { 0, 1, 1, 1, 0, -1 };
    int[] evenDirX2 = { -1, 0, 1, 2, 2, 2, 1, 0, -1, -1, -2, -1 };
    int[] evenDirX3 = { -1, 0, 1, 2, 2, 3, 3, 3, 2, 2, 1, 0, -1, -2, -2, -3, -2, -2 };

    int[] oddDirX1 = { -1, 0, 1, 0, -1, -1 };
    int[] oddDirX2 = { -1, 0, 1, 2, 2, 2, 1, 0, -1, -1, -2, -1 };
    int[] oddDirX3 = { -2, -1, 0, 1, 2, 2, 3, 2, 2, 1, 0, -1, -2, -2, -3, -3, -3, -2 };

    int[] DirY1 = { -1, -1, 0, 1, 1, 0 };
    int[] DirY2 = { -2, -2, -2, -1, 0, 1, 2, 2, 2, 1, 0, -1 };
    int[] DirY3 = { -3, -3, -3, -3, -2, -1, 0, 1, 2, 3, 3, 3, 3, 2, 1, 0, -1, -2 };

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
    public HexaMapNode GetNode(int x, int y) //need offset
    {
        return hexgrid[x, y];
    }
    public void SetWalkable(int x, int y, bool walkable)
    {
        walkables[x, y] = walkable;
    }
    public bool[,] GetWalkable()
    {
        return walkables;
    }
    public bool GetWalkable(int x, int y)
    {
        return walkables[x, y];
    }
    public CellPositionCalc GetCellPosCalc()
    {
        return cellPositionCalc;
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

        //Debug.Log($"evenDirx1 : {evenDirX1.Length} evenDirx2 : {evenDirX2.Length} evenDirx3 : {evenDirX3.Length}");
        //Debug.Log($"oddDirx1 : {oddDirX1.Length} oddDirx2 : {oddDirX2.Length} oddDirx3 : {oddDirX3.Length}");
        //Debug.Log($"DirY1 : {DirY1.Length} DirY2 : {DirY2.Length} DirY3 : {DirY3.Length}");

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
            Debug.Log(i);
            evenDirX[2][i] = evenDirX3[i];
            oddDirX[2][i] = oddDirX3[i];
            DirY[2][i] = DirY3[i];
        }

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
    public bool IsBreakable(HexaMapNode node)
    {
        int cnt = 0;
        if (!node.GetBreakable())
        {
            return false;
        }
        List<HexaMapNode> list = GetNeighborNode(node.GetGridPos().x, node.GetGridPos().y, 1);
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].GetTileType() == TileType.Path)
            {
                cnt++;
                if (cnt == 2)
                {
                    return false;
                }
            }
            else
            {
                cnt = 0;
            }
        }
        if (cnt == 1 || list[0].GetTileType() == TileType.Path)
            return false;
        return true;
    }
    private bool CheckRoomNode(HexaMapNode node)
    {
        if (node == null)
        {
            return false;
        }
        TileType type = node.GetTileType();
        if (type == TileType.RoomCenter || type == TileType.RoomNode || (!node.GetBreakable()&& type!= TileType.Path))
            return false;
        return true;
    }
    private bool CheckRoomWall(HexaMapNode node)
    {
        if (node == null)
        {
            return false;
        }
        TileType type = node.GetTileType();
        if (type == TileType.RoomCenter || type == TileType.RoomNode)
            return false;
        return true;
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
    private void MakeGrid()
    {
        CalcMapSize();
        hexgrid = new HexaMapNode[GetMapSizeX(), GetMapSizeY()];
        walkables = new bool[GetMapSizeX(), GetMapSizeY()];
        Vector2Int offset = cellPositionCalc.GetOffset();
        for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(pos) != null)
                {
                    string name = tilemap.GetTile(pos).name;
                    HexaMapNode node = tileFactory.GetNode(name);
                    node.Start();
                    node.SetGridPos(new Vector2Int(x + offset.x, y + offset.y));
                    node.SetCellPos(pos);
                    node.SetWorldPos(tilemap.CellToWorld(pos));
                    SetWalkable(x + offset.x, y + offset.y, node.GetWalkable());
                    SetNode(x + offset.x, y + offset.y, node);
                }
            }
        }
    }
    #endregion
    #region Neighbor
    private List<HexaMapNode> GetNeighborNode(int x, int y, int phase = 1) // max phase = 3 min phase = 1
    {
        List<HexaMapNode> neighbors = new();
        for (int p = 0; p < phase; p++)
        {
            List<HexaMapNode> list = GetNeighborRingNode(x, y, p);
            for( int i = 0;i < list.Count; i++)
            {
                neighbors.Add(list[i]);
            }
        }

        return neighbors;
    }
    public List<HexaMapNode> GetNeighborWalkableNode(int x, int y)
    {
        int idxX, idxY;
        List<HexaMapNode> neighbors = new();
        for (int i = 0; i < 6; i++)
        {
            idxX = x + evenDirX1[i];
            idxY = y + DirY1[i];
            if (IsNodeExist(idxX, idxY) && GetNode(idxX, idxY).GetWalkable())
            {
                neighbors.Add(GetNode(idxX, idxY));
            }
        }

        return neighbors;
    }
    private List<HexaMapNode> GetNeighborRingNode(int x, int y, int phase)
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
    #endregion
    #region TileSwap
    public void SwapNode(int x, int y, string tile)
    {
        HexaMapNode prevNode = GetNode(x, y);
        HexaMapNode node = tileFactory.GetNode(tile);
        node.Start();
        node.SetNodePosition(prevNode);
        SetNode(x, y, node);
        tilemap.SetTile(node.GetCellPos(), tileFactory.GetTile(((int)node.GetTileType())));
    }
    #endregion
    #region Room
    public bool MakeRoom(HexaMapNode RoomCenter)
    {
        Vector2Int centerPos = RoomCenter.GetGridPos();
        List<HexaMapNode> nodes = GetNeighborNode(centerPos.x, centerPos.y, 1);

        // if not exist node is contain return null
        if (nodes.Count != roomNodeNum[0])
            return false;
        for (int i = 0; i < nodes.Count; i++)
        {
            if (!CheckRoomNode(nodes[i]))
                return false;
        }
        List<HexaMapNode> rings = GetNeighborRingNode(centerPos.x, centerPos.y, 1);
        if (rings.Count != ringNodeNum[1])
            return false;
        for (int i = 0; i < rings.Count; i++)
        {
            if (!CheckRoomNode(rings[i]))
                return false;
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector2Int nodePos = nodes[i].GetGridPos();
            SwapNode(nodePos.x, nodePos.y, "RoomNode");
        }
        SwapNode(centerPos.x, centerPos.y, "RoomCenter");
        return true;
    }
    #endregion
    #region Unity Function
    private void Awake()
    {
        SetDirection();
        cellPositionCalc = new CellPositionCalc();
        tileFactory = GetComponent<NodeFactory>();
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();

        MakeGrid();
    }
    #endregion
}