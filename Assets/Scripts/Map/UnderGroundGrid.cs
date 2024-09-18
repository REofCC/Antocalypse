using System.Collections.Generic;
using UnityEngine;

public class UnderGroundGrid : MonoBehaviour
{
    #region attribute
    private int xSize;
    private int ySize;
    private int tileSize;
    private BaseMapNode[,] grid;
    private bool[,] walkables;
    private Vector2 gridStartPoint;
    private GameObject map;
    #endregion

    #region Setter & Getter

    public void SetMapSize(int xSize, int ySize)
    {
        this.xSize = xSize;
        this.ySize = ySize;
    }
    public int GetXsize()
    {
        return xSize;
    }
    public int GetYsize()
    {
        return ySize;
    }
    public int GetTileSize()
    {
        return tileSize;

    }
    public void SetTileSize(int tileSize)
    {
        this.tileSize = tileSize;
    }
    public bool SetMap()
    {
        map = GameObject.Find("UnderGroundMap");
        if (map == null)
            return false;
        return true;
    }
    public GameObject GetMap()
    {
        return map;
    }
    public BaseMapNode GetNode(int posx, int posy)
    {
        return grid[posx, posy];
    }

    public void SetNode(BaseMapNode node, int posx, int posy)
    {
        Debug.Log(posx + " | " + posy + " | " + node.GetTileInfo().TileName);
        grid[posx, posy] = node;
        walkables[posx, posy] = node.GetWalkable();
    }
    public Vector2 GetStartPoint()
    {
        return gridStartPoint; 
    }
    public void SetStartPoint(Vector2 pos)
    {
        gridStartPoint = pos;
    }

    public bool[,] GetWalkables()
    {
        return walkables;
    }
    #endregion

    #region Function
    public GameObject[] GetTiles()
    {
        GameObject[] tiles = new GameObject[map.transform.childCount];
        for (int i = 0; i < map.transform.childCount; i++)
        {
            tiles[i] = map.transform.GetChild(i).gameObject;
        }
        return tiles;
    }
    public int CalcXaxis(float xpos)
    {
        int pos = Mathf.RoundToInt((xpos - gridStartPoint.x) / tileSize);
        return pos;
    }
    public int CalcYaxis(float ypos)
    {
        int pos = Mathf.RoundToInt((ypos - gridStartPoint.y) / tileSize);
        return pos;
    }
    public void MakeGrid()
    {
        grid = new BaseMapNode[xSize, ySize];
        walkables = new bool[xSize, ySize];
        GameObject[] tiles = GetTiles();
        GameObject tile;
        int xpos, ypos;
        for (int i = 0; i < tiles.Length; i++)
        {
            tile = tiles[i];
            xpos = CalcXaxis(tile.gameObject.transform.position.x);
            ypos = CalcYaxis(tile.gameObject.transform.position.y);
            SetNode(tile.GetComponent<BaseMapNode>(), xpos, ypos);
        }
    }
    public bool IsTileExist(int idxX, int idxY)
    {
        if(idxX < 0 || idxY < 0 || idxX > xSize || idxY > ySize)
        {
            return false;
        }
        return true;
    }
    public List<BaseMapNode> GetNeighbor(float posx, float posy, int radius)
    {
        int idxX = CalcXaxis(posx);
        int idxY = CalcYaxis(posy);
        List<BaseMapNode> neighbor = new List<BaseMapNode>();

        for(int i = idxX - radius; i <= idxX + radius; i++)
        {
            for (int j = idxY - radius; j <= idxY + radius; j++)
            {
                if(IsTileExist(i, j))
                {
                    neighbor.Add(GetNode(i,j));
                }
            }
        }
        return neighbor;
    }

    public List<BaseMapNode> GetNeighborForAgrid(float posx, float posy)
    {
        int radius = 1;
        int idxX = CalcXaxis(posx);
        int idxY = CalcYaxis(posy);
        List<BaseMapNode> neighbor = new List<BaseMapNode>();

        for (int i = idxX - radius; i <= idxX + radius; i++)
        {
            for (int j = idxY - radius; j <= idxY + radius; j++)
            {
                if (IsTileExist(i, j))
                {
                    if(GetNode(i, j).GetWalkable())
                        neighbor.Add(GetNode(i, j));
                }
            }
        }
        return neighbor;
    }
    #endregion

    #region ChangeNode
    public void ChangeNode(BaseMapNode node, float posx, float posy) 
    { 
        int idxX = CalcXaxis(posx);
        int idxY = CalcYaxis(posy);
        SetNode(node, idxX, idxY);
    }
    #endregion

    private void Start()
    {
        SetStartPoint(this.gameObject.transform.position);
        SetTileSize(1);
        SetMapSize(2, 2);
        SetMap();
        MakeGrid();
        List<BaseMapNode> Neighbor = GetNeighbor(0,0,1);

        foreach (BaseMapNode neighbor in Neighbor)
        {
            Debug.Log(neighbor.GetTileInfo().TileName);
        }
    }
}
