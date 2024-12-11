using UnityEngine;
using UnityEngine.Tilemaps;

public class MapMaker : MonoBehaviour
{
    #region Attribute
    Tilemap underTilemap;
    HexaGrid underGrid;

    Tilemap upTilemap;
    HexaGrid upGrid;

    NodeFactory nodeFactory;
    ResourceFactory resourceFactory;
    RoomFactory roomFactory;
    EventFactory eventFactory;

    int mapSize;
    HexaMapNode startPos;
    #endregion

    #region Function
    public HexaMapNode GetStartPos()
    {
        return startPos;
    }
    private void MakeBase()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                underTilemap.SetTile(new Vector3Int(x, y, 0), nodeFactory.GetTile(0));
                upTilemap.SetTile(new Vector3Int(x,y,100), nodeFactory.GetTile(5));
            }
        }
        MapManager.Map.BlackMask.FillMask(mapSize);
        upGrid.MakeGrid();
        underGrid.MakeGrid();
        
    }
    private void MakeStartPos()
    {
        startPos = underGrid.GetNode(mapSize / 2 + 1, mapSize / 2 + 1);
        roomFactory.MakeRoom(startPos);
    }
    private void MakeDoorNode()
    {
        float distance = 0;
        float standard = underTilemap.cellSize.x * 5f;
        float standard2 = underTilemap.cellSize.x * 10f;
        int posx = 0;
        int posy = 0;
        do
        {
            posx = Random.Range(0, mapSize);
            posy = Random.Range(0, mapSize);
            Vector3 StartPos = underGrid.GetNode(15, 15).GetWorldPos();
            Vector3 pos = underGrid.GetNode(posx, posy).GetWorldPos();
            distance = Vector3.Distance(StartPos, pos);
        } while (distance <= standard && distance >= standard2);

        DoorNode node = (DoorNode)underGrid.SwapNode(posx, posy, "DoorNode", false);
        underGrid.SetDoorPos(node);
        node = (DoorNode)upGrid.SwapNode(posx, posy, "DoorNode", false);
        upGrid.SetDoorPos(node);
    }
    private void MakeResource()
    {
        HexaMapNode[,] hexgrid = underGrid.GetGrid();
        Vector3 startPos = underGrid.GetNode(15, 15).GetWorldPos();
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                HexaMapNode node = underGrid.GetNode(x, y);
                float distance = Vector3.Distance(startPos, node.GetWorldPos());
                resourceFactory.MakeResource(node, distance, underTilemap.cellSize.x);
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
        underTilemap = MapManager.Map.UnderTileMap;
        underGrid = MapManager.Map.UnderGrid;

        upTilemap = MapManager.Map.UpTileMap;
        upGrid = MapManager.Map.UpGrid;

        nodeFactory = MapManager.Map.NodeFactory;
        resourceFactory = MapManager.Map.ResourceFactory;
        roomFactory = MapManager.Map.RoomFactory;
        this.mapSize = mapSize;
    }
    #endregion
}
