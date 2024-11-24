using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    #region SingleTon
    private static MapManager mapManager;
    public static MapManager Map { get { return mapManager; } }
    #endregion

    #region Attribute
    int mapSize = 31;
    //underground map
    HexaGrid underGrid;
    Tilemap undermap;

    //ground map
    HexaGrid upGrid; 
    Tilemap upmap;

    MapMaker mapMaker;

    //UnderGround Factory
    BuildingFactory buildingFactory;
    ResourceFactory resourceFactory;
    UnderGroundNodeFactory underGroundNodeFactory;
    RoomFactory roomFactory;

    //Ground Factory
    EnemyFactory enemyFactory;
    EventFactory eventFactory;

    //Tool
    HexaPathFinder pathFinder;
    CellPositionCalc positionCalc;

    #endregion

    #region Getter
    public HexaGrid UnderGrid { get { return underGrid; } }
    public Tilemap UnderTileMap { get { return undermap; } }
    public HexaGrid UpGrid { get {return upGrid; } }
    public Tilemap UpTileMap { get { return upmap; } }
    public MapMaker MapMaker { get { return mapMaker; } }
    public BuildingFactory BuildingFactory { get { return buildingFactory; } }
    public ResourceFactory ResourceFactory { get {return resourceFactory; } }
    public UnderGroundNodeFactory UnderNodeFactory { get { return underGroundNodeFactory;} }
    public RoomFactory RoomFactory { get { return roomFactory; } }
    public EnemyFactory EnemyFactory { get {return enemyFactory;} }
    public EventFactory EventFactory { get {return eventFactory; } }
    public HexaPathFinder PathFinder { get { return pathFinder; } }
    public CellPositionCalc PositionCalc { get { return positionCalc; } }
    #endregion

    #region Function
    private bool SetFactories()
    {
        GameObject go = GameObject.Find("Factory");
        buildingFactory = go.GetComponent<BuildingFactory>();
        resourceFactory = go.GetComponent<ResourceFactory>();
        underGroundNodeFactory = go.GetComponent<UnderGroundNodeFactory>();
        roomFactory = go.GetComponent<RoomFactory>();
        enemyFactory = go.GetComponent<EnemyFactory>();
        eventFactory = go.GetComponent<EventFactory>();

        ResourceFactory.OnAwake(mapSize);
        RoomFactory.OnAwake();
        return true;
    }

    private bool SetTools()
    {
        GameObject go = GameObject.Find("MapTool");
        pathFinder = go.GetComponent<HexaPathFinder>();
        positionCalc = new();
        mapMaker = go.GetComponent<MapMaker>();

        pathFinder.OnAwake();
        mapMaker.OnAwake(mapSize);
        return true;
    }

    private bool SetGrid()
    {
        GameObject go = GameObject.Find("Grid");
        undermap = go.transform.GetChild(0).GetComponent<Tilemap>();

        go = GameObject.Find("MapTool");
        underGrid = go.GetComponent<HexaGrid>();
        return true;
    }
    #endregion

    #region UnityFunction
    private void Start()
    {
        mapManager = GetComponent<MapManager>();
        if (!SetGrid())
        {
            Debug.Log("Error : Grid Missing");
        }
        if (!SetFactories())
        {
            Debug.Log("Error : Factory Missing");
        }
        if (!SetTools())
        {
            Debug.Log("Error : Tool Missing");
        }
    }
    #endregion
}
