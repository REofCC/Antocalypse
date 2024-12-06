using UnityEngine;
using UnityEngine.Tilemaps;

public class BlackMask : MonoBehaviour
{
    #region Attribute
    Tilemap tilemap;
    [SerializeField]
    TileBase maskTile;
    #endregion
    #region Direction
    readonly int[] oddDirX = { 0, 1, 1, 1, 0, -1 };
    readonly int[] evenDirX = { -1, 0, 1, 0, -1, -1 };
    readonly int[] DirY = { -1, -1, 0, 1, 1, 0 };
    #endregion

    #region Getter
    #endregion

    #region Function
    public void EraseNeighborNode(int x, int y)
    {
        int posx, posy;
        EraseTile(x, y);
        if (y % 2 == 0)
        {
            for(int i = 0; i < 6; i++)
            {
                posx = x + evenDirX[i];
                posy = y + DirY[i];
                EraseTile(posx, posy);
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                posx = x + oddDirX[i];
                posy = y + DirY[i];
                EraseTile(posx, posy);
            }
        }
    }

    private void EraseTile(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y, 0);
        if (tilemap.GetTile(pos)!= null)
        {
            tilemap.SetTile(pos, null);
        }
    }
    public void FillMask(int mapSize)
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                MakeMask(x, y);
            }
        }
    }

    private void MakeMask(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y, 0);
        tilemap.SetTile(pos, maskTile);
    }
    #endregion

    public void OnAwake()
    {
        GameObject go = GameObject.Find("Grid").transform.GetChild(2).gameObject;
        tilemap = go.GetComponent<Tilemap>();
    }
}
