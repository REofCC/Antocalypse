
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
    ResourceFactory resFactory;

    HexaMapNode[,] hexgrid;
    bool[,] walkables;

    int mapSizeX;
    int mapSizeY;
    #endregion
    #region Direction for Get Node Circlular Rotation
    //Unity Cell System = Even-r Horizontal Layout
    readonly int[] ringNodeNum = { 6, 12, 18 };

    readonly int[] roomNodeNum = { 6, 18 };

    int[][] oddDirX;
    int[][] evenDirX;
    int[][] DirY;

    readonly int[] evenDirX1 = { 0, 1, 1, 1, 0, -1 };
    readonly int[] evenDirX2 = { -1, 0, 1, 2, 2, 2, 1, 0, -1, -1, -2, -1 };
    readonly int[] evenDirX3 = { -1, 0, 1, 2, 2, 3, 3, 3, 2, 2, 1, 0, -1, -2, -2, -3, -2, -2 };

    readonly int[] oddDirX1 = { -1, 0, 1, 0, -1, -1 };
    readonly int[] oddDirX2 = { -1, 0, 1, 2, 2, 2, 1, 0, -1, -1, -2, -1 };
    readonly int[] oddDirX3 = { -2, -1, 0, 1, 2, 2, 3, 2, 2, 1, 0, -1, -2, -2, -3, -3, -3, -2 };

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
    private bool CheckRoomNode(List<HexaMapNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            TileType type = nodes[i].GetTileType();
            if (type == TileType.RoomCenter || type == TileType.RoomNode || (!nodes[i].GetBreakable() && type != TileType.Path))
                return false;
        }
        return true;
    }
    private bool CheckRoomWall(List<HexaMapNode> rings)
    {
        for (int i = 0; i < rings.Count; i++)
        {
            TileType type = rings[i].GetTileType();
            if (type == TileType.RoomCenter || type == TileType.RoomNode)
                return false;
        }
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
    public void MakeGrid()
    {
        CalcMapSize();
        hexgrid = new HexaMapNode[GetMapSizeX(), GetMapSizeY()];
        walkables = new bool[GetMapSizeX(), GetMapSizeY()];
        Vector2Int offset = cellPositionCalc.GetOffset();
        for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
            {
                Vector3Int pos = new(x, y, 0);
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
            for (int i = 0; i < list.Count; i++)
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
    private List<HexaMapNode> GetNeighborRingNode(int x, int y, int phase) // max phase = 0 min phase = 2
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
        if (nodes.Count != roomNodeNum[0])
            return false;
        if (!CheckRoomNode(nodes))
            return false;

        List<HexaMapNode> rings = GetNeighborRingNode(centerPos.x, centerPos.y, 1);
        if (rings.Count != ringNodeNum[1])
            return false;
        if (!CheckRoomWall(rings))
            return false;

        SwapNode(centerPos.x, centerPos.y, "RoomCenter");
        RoomCenter center = (RoomCenter)GetNode(centerPos.x, centerPos.y);
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector2Int nodePos = nodes[i].GetGridPos();
            SwapNode(nodePos.x, nodePos.y, "RoomNode");
            RoomNode node = (RoomNode)GetNode(nodePos.x, nodePos.y);
            center.AddRoomNode(node);
            node.SetCenter(center);
        }
        return true;
    }
    public bool ExpandRoom(RoomCenter roomCenter, int phase = 2)
    {
        if (roomCenter.GetTileType() != TileType.RoomCenter)
            return false;
        Vector2Int centerPos = roomCenter.GetGridPos();
        List<HexaMapNode> nodes = GetNeighborRingNode(centerPos.x, centerPos.y, phase - 1);
        if (nodes.Count != ringNodeNum[phase - 1])
            return false;
        if (CheckRoomNode(nodes))
            return false;

        List<HexaMapNode> rings = GetNeighborRingNode(centerPos.x, centerPos.y, phase);
        if (rings.Count != ringNodeNum[phase])
            return false;
        if (!CheckRoomWall(rings))
            return false;
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector2Int nodePos = nodes[i].GetGridPos();
            SwapNode(nodePos.x, nodePos.y, "RoomNode");
            RoomNode node = (RoomNode)GetNode(nodePos.x, nodePos.y);
            roomCenter.AddRoomNode(node);
            node.SetCenter(roomCenter);
        }
        return true;
    }
    #endregion

    #endregion
    #region Unity Function
    public void OnAwake()
    {
        SetDirection();
        cellPositionCalc = new CellPositionCalc();

        MapMaker maker = GetComponent<MapMaker>();
        tileFactory = maker.GetNodeFactory();
        resFactory = maker.GetResourceFactory();
        tilemap = maker.GetTileMap();
    }
    #endregion
}