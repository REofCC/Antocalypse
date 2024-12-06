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
    NodeFactory nodeFactory;
    RoomFactory roomFactory;

    //Ground Factory
    EnemyFactory enemyFactory;
    EventFactory eventFactory;

    //Tool
    HexaPathFinder pathFinder;
    CellPositionCalc upPosCalc;
    CellPositionCalc underPosCalc;

    #endregion

    #region Getter
    public HexaGrid UnderGrid { get { return underGrid; } }
    public Tilemap UnderTileMap { get { return undermap; } }
    public HexaGrid UpGrid { get {return upGrid; } }
    public Tilemap UpTileMap { get { return upmap; } }
    public MapMaker MapMaker { get { return mapMaker; } }
    public BuildingFactory BuildingFactory { get { return buildingFactory; } }
    public ResourceFactory ResourceFactory { get {return resourceFactory; } }
    public NodeFactory NodeFactory { get { return nodeFactory;} }
    public RoomFactory RoomFactory { get { return roomFactory; } }
    public EnemyFactory EnemyFactory { get {return enemyFactory;} }
    public EventFactory EventFactory { get {return eventFactory; } }
    public HexaPathFinder PathFinder { get { return pathFinder; } }
    public CellPositionCalc UpPosCalc { get { return UpPosCalc; } }
    public CellPositionCalc UnderPosCalc {  get { return underPosCalc; } }
    #endregion

    #region Function
    private bool SetFactories()
    {
        GameObject go = GameObject.Find("Factory");
        buildingFactory = go.GetComponent<BuildingFactory>();
        resourceFactory = go.GetComponent<ResourceFactory>();
        nodeFactory = go.GetComponent<NodeFactory>();
        roomFactory = go.GetComponent<RoomFactory>();
        enemyFactory = go.GetComponent<EnemyFactory>();
        eventFactory = go.GetComponent<EventFactory>();

        return true;
    }

    private bool SetTools()
    {
        GameObject go = GameObject.Find("MapTool");
        pathFinder = go.GetComponent<HexaPathFinder>();
        mapMaker = go.GetComponent<MapMaker>();

        return true;
    }

    private bool SetGrid()
    {
        GameObject go = GameObject.Find("Grid");
        undermap = go.transform.GetChild(0).GetComponent<Tilemap>();
        upmap = go.transform.GetChild(1).GetComponent<Tilemap>();
        go = GameObject.Find("MapTool");
        underGrid = go.GetComponent<HexaGrid>();
        go = GameObject.Find("GroundTool");
        upGrid = go.GetComponent<HexaGrid>();

        return true;
    }

    private bool OnAwake()
    {
        underGrid.OnAwake(undermap, underPosCalc);
        upGrid.OnAwake(upmap, upPosCalc);
        pathFinder.OnAwake();
        mapMaker.OnAwake(mapSize);
        ResourceFactory.OnAwake(mapSize);
        RoomFactory.OnAwake();

        return true;
    }
    #endregion

    #region UnityFunction
    private void Start()
    {
        mapManager = GetComponent<MapManager>();
        upPosCalc = new();
        underPosCalc = new();
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
        if (!OnAwake())
        {
            Debug.Log("Error : Awake Missing");
        }
        MapMaker.MapMaking();
    }
    #endregion
}

