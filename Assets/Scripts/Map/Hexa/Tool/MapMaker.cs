using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapMaker : MonoBehaviour
{
    #region Attribute
    Tilemap tilemap;
    NodeFactory nodeFactory;
    ResourceFactory resourceFactory;
    HexaGrid grid;
    int mapSize = 31;
    #endregion

    #region Getter & Setter
    public Tilemap GetTileMap()
    {
        return tilemap;
    }
    public NodeFactory GetNodeFactory()
    {
        return nodeFactory;
    }
    public ResourceFactory GetResourceFactory()
    {
        return resourceFactory;
    }
    public HexaGrid GetGrid()
    {
        return grid;
    }
    #endregion

    #region Function
    private void MakeBase()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), nodeFactory.GetTile(0));
            }
        }
        grid.MakeGrid();
    }
    private void MakeStartPos()
    {
        HexaMapNode node = grid.GetNode(16, 16);
        grid.MakeRoom(node);
    }
    private void MakeResource()
    {

    }
    private void MapMaking()
    {
        MakeBase();
        MakeStartPos();
    }
    #endregion

    #region Unity Function
    public void Awake()
    {
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();
        grid = GetComponent<HexaGrid>();
        GameObject tool = gameObject.transform.GetChild(0).gameObject;
        nodeFactory = tool.GetComponent<NodeFactory>();
        resourceFactory = tool.GetComponent<ResourceFactory>();

        grid.OnAwake();
        MapMaking();
    }
    #endregion
}
