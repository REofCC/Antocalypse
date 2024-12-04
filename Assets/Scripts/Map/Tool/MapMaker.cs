using UnityEngine;
using UnityEngine.Tilemaps;


public class MapMaker : MonoBehaviour
{
    #region Attribute
    Tilemap tilemap;
    UnderGroundNodeFactory nodeFactory;
    ResourceFactory resourceFactory;
    RoomFactory roomFactory;
    HexaGrid grid;
    int mapSize;
    #endregion

    #region Getter & Setter
    public Tilemap GetTileMap()
    {
        return tilemap;
    }
    public UnderGroundNodeFactory GetNodeFactory()
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
        HexaMapNode node = grid.GetNode(15, 15);
        roomFactory.MakeRoom(node);
    }
    private void MakeDoorNode()
    {
        float distance = 0;
        float standard = tilemap.cellSize.x * 5f;
        float standard2 = tilemap.cellSize.x * 10f;
        int posx = 0;
        int posy = 0;
        do
        {
            posx = Random.Range(0, mapSize);
            posy = Random.Range(0, mapSize);
            Vector3 StartPos = grid.GetNode(15, 15).GetWorldPos();
            Vector3 pos = grid.GetNode(posx, posy).GetWorldPos();
            distance = Vector3.Distance(StartPos, pos);
        } while (distance <= standard && distance >= standard2);

        DoorNode node = (DoorNode)grid.SwapNode(posx, posy, "DoorNode", false);
        grid.SetDoorPos(node);
    }
    private void MakeResource()
    {
        HexaMapNode[,] hexgrid = grid.GetGrid();
        Vector3 startPos = grid.GetNode(15, 15).GetWorldPos();
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                HexaMapNode node = grid.GetNode(x, y);
                float distance = Vector3.Distance(startPos, node.GetWorldPos());
                resourceFactory.MakeResource(node, distance, tilemap.cellSize.x);
            }
        }
    }
    public void MapMaking()
    {
        MakeBase();
        MakeDoorNode();
        MakeStartPos();
        MakeResource();
    }
    #endregion

    #region Unity Function
    public void OnAwake(int mapSize)
    {
        tilemap = MapManager.Map.UnderTileMap;
        grid = MapManager.Map.UnderGrid;
        nodeFactory = MapManager.Map.UnderNodeFactory;
        resourceFactory = MapManager.Map.ResourceFactory;
        roomFactory = MapManager.Map.RoomFactory;
        this.mapSize = mapSize;
    }
    #endregion
}
