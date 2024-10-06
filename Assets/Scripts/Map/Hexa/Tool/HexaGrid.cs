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
    bool[,] walkables;

    int mapSizeX;
    int mapSizeY;
    #endregion
    #region Direction for Get Node Circlular Rotation
    int[] oddDirX = {0,1,1,1,0,-1 };
    int[] evenDirX = {-1,0,1,0,-1,-1 };
    int[] DirY = {-1,-1,0,1,1,0 };
    #endregion
    #region Offset for Get Room Node
    int[,] roomDirX = { { 0, 1 }, { -1, 1 }, { -1, 2 }, { -2, 2 }, { -2, -3 }, { -3, 3 } };
    int[,] X_Phase = { { 0, 1 }, { 1, 3 }, { 2, 5 } };
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
        walkables[x,y] = walkable;
    }
    public bool[,] GetWalkable()
    {
        return walkables;
    }
    public bool GetWalkable(int x, int y)
    {
        return walkables[x,y];
    }
    public CellPositionCalc GetCellPosCalc()
    {
        return cellPositionCalc;
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
    private bool IsBreakable(HexaMapNode node)
    {
        int cnt = 0;
        if (node.GetTileType() != (int)TileType.Wall)
        {
            return false;
        }
        List<HexaMapNode> list = GetNeighborNode(node.GetGridPos().x, node.GetGridPos().y);
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].GetTileType() == TileType.Path)
            {
                cnt++;
                if(cnt == 2)
                {
                    return false;
                }
            }
            else
            {
                cnt = 0;
            }
        }
        return true;
    }
    private bool CheckRoom(HexaMapNode node)
    {
        if(node == null)
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
                if(tilemap.GetTile(pos)!= null)
                {
                    string name = tilemap.GetTile(pos).name;
                    HexaMapNode node = tileFactory.GetNode(name);
                    node.Start();
                    node.SetGridPos(new Vector2Int(x+ offset.x, y+offset.y));
                    node.SetCellPos(pos);
                    node.SetWorldPos(tilemap.CellToWorld(pos));
                    SetWalkable(x + offset.x, y + offset.y, node.GetWalkable());
                    Debug.Log(name);
                    SetNode(x + offset.x, y + offset.y, node);
                }
            }
        }
    }
    #endregion
    #region Neighbor
    public List<HexaMapNode> GetNeighborNode(int x, int y)
    {
        int idxX, idxY;
        List<HexaMapNode> neighbors = new();
        if(x%2 == 0)
        {
            for(int i = 0; i < 6; i++)
            {
                idxX = x + evenDirX[i];
                idxY = y+ DirY[i];
                if(IsNodeExist(idxX, idxY))
                {
                    neighbors.Add(GetNode(idxX, idxY));
                }
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                idxX = x + oddDirX[i];
                idxY = y + DirY[i];
                if (IsNodeExist(idxX, idxY))
                {
                    neighbors.Add(GetNode(idxX, idxY));
                }
            }
        }
        return neighbors;
    }
    public List<HexaMapNode> GetNeighborWalkableNode(int x, int y)
    {
        int idxX, idxY;
        List<HexaMapNode> neighbors = new();
        if (x % 2 == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                idxX = x + evenDirX[i];
                idxY = y + DirY[i];
                if (IsNodeExist(idxX, idxY) && GetNode(idxX,idxY).GetWalkable())
                {
                    neighbors.Add(GetNode(idxX, idxY));
                }
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                idxX = x + oddDirX[i];
                idxY = y + DirY[i];
                if (IsNodeExist(idxX, idxY) && GetNode(idxX, idxY).GetWalkable())
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
        node.SetNodePosition(prevNode);
        tilemap.SetTile(node.GetCellPos(), tileFactory.GetTile(((int)node.GetTileType())));
    }
    #endregion
    #region Room

    #endregion
    #endregion
    #region Unity Function
    private void Awake()
    {
        cellPositionCalc = new CellPositionCalc();
        tileFactory = GetComponent<NodeFactory>();
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();

        MakeGrid();
    }
    #endregion
}