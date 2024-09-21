using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    #region attribute

    private GameObject undergroundMap;

    private UnderGroundGrid underGroundGrid;

    //private int Xsize;
    //private int Ysize;
    #endregion
    public void Start()
    {
        undergroundMap = GameObject.Find("UnderGroundMap");

        underGroundGrid = GameObject.Find("Grid").GetComponent<UnderGroundGrid>();

    }
    #region Prefab Instantiate & Destroy
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            //Debug.Log($"Prefab Not Found : {path}");
            return null;
        }
        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go != null)
        {
            Object.Destroy(go);
        }
    }
    #endregion

    #region Tile
    private void PositionTile(GameObject tile,float posx, float posy)
    {
        tile.transform.position = new Vector2(posx, posy);
    }

    private void SetTileOnGrid(GameObject tile)
    {
        underGroundGrid.SetNode(tile.GetComponent<BaseMapNode>());
    }
    public GameObject CreateTile(string name, float posx, float posy)
    {
        GameObject tile = Instantiate($"Map/{name}", undergroundMap.transform);
        PositionTile(tile, posx, posy);
        SetTileOnGrid(tile);
        return tile;
    }
    #endregion
    #region Map
    /*
    private void SetMapsize(int x, int y)
    {
        Xsize = x;
        Ysize = y;
    }
    private void CreateMap()
    {
    }

    #endregion
    */
    #endregion
}
