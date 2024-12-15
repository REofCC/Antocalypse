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
    //state
    StateManager stateManager;

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
    //RoomFactory roomFactory;

    //Ground Factory
    EnemyFactory enemyFactory;
    EventFactory eventFactory;

    //Tool
    HexaPathFinder underPathFinder;
    HexaPathFinder upPathFinder;
    CellPositionCalc upPosCalc;
    CellPositionCalc underPosCalc;
    BlackMask underBlackMask;
    BlackMask upBlackMask;
    TravelTrail travelTrail;
    //Layer
    LayerType layerType;
    #endregion

    #region Getter
    public StateManager State {  get { return stateManager; } }
    public HexaGrid UnderGrid { get { return underGrid; } }
    public Tilemap UnderTileMap { get { return undermap; } }
    public HexaGrid UpGrid { get {return upGrid; } }
    public Tilemap UpTileMap { get { return upmap; } }
    public MapMaker MapMaker { get { return mapMaker; } }
    public BuildingFactory BuildingFactory { get { return buildingFactory; } }
    public ResourceFactory ResourceFactory { get {return resourceFactory; } }
    public NodeFactory NodeFactory { get { return nodeFactory;} }
    //ublic RoomFactory RoomFactory { get { return roomFactory; } }
    public EnemyFactory EnemyFactory { get {return enemyFactory;} }
    public EventFactory EventFactory { get {return eventFactory; } }
    public HexaPathFinder UnderPathFinder { get { return underPathFinder; } }
    public HexaPathFinder UpPathFinder { get { return upPathFinder; } }
    public CellPositionCalc UpPosCalc { get { return upPosCalc; } }
    public CellPositionCalc UnderPosCalc {  get { return underPosCalc; } }
    public BlackMask UnderBlackMask { get { return underBlackMask; } }
    public BlackMask UpBlackMask { get { return upBlackMask; } }
    public LayerType LayerType { get { return layerType; } }

    public TravelTrail TravelTrail { get { return travelTrail; } }

    #endregion

    #region Function
    private bool SetFactories()
    {
        GameObject go = GameObject.Find("Factory");
        buildingFactory = go.GetComponent<BuildingFactory>();
        resourceFactory = go.GetComponent<ResourceFactory>();
        nodeFactory = go.GetComponent<NodeFactory>();
        //roomFactory = go.GetComponent<RoomFactory>();
        enemyFactory = go.GetComponent<EnemyFactory>();
        eventFactory = go.GetComponent<EventFactory>();

        return true;
    }

    private bool SetTools()
    {
        stateManager = GetComponent<StateManager>();
        GameObject go = GameObject.Find("MapTool");
        underPathFinder = go.GetComponent<HexaPathFinder>();
        mapMaker = go.GetComponent<MapMaker>();
        underBlackMask = go.GetComponent<BlackMask>();
        go = GameObject.Find("GroundTool");
        upPathFinder = go.GetComponent<HexaPathFinder>();
        upBlackMask = go.GetComponent<BlackMask>();
        travelTrail = new();
        travelTrail.OnAwake(go.GetComponent<LineRenderer>());
        return true;
    }

    private bool SetGrid()
    {
        GameObject go = GameObject.Find("UnderGroundGrid");
        undermap = go.transform.GetChild(0).GetComponent<Tilemap>();
        go = GameObject.Find("GroundGrid");
        upmap = go.transform.GetChild(0).GetComponent<Tilemap>();
        go = GameObject.Find("MapTool");
        underGrid = go.GetComponent<HexaGrid>();
        go = GameObject.Find("GroundTool");
        upGrid = go.GetComponent<HexaGrid>();

        return true;
    }

    //[LSH:CODE] [node-order-ui] ������ ���� ������ ���� �߰�
    public void SetLayerType(LayerType _layerType)
    {
        layerType = _layerType;
    }

    private bool OnAwake()
    {
        underGrid.OnAwake(undermap, underPosCalc, underBlackMask);
        upGrid.OnAwake(upmap, upPosCalc, upBlackMask);
        underPathFinder.OnAwake(underGrid);
        upPathFinder.OnAwake(upGrid);
        mapMaker.OnAwake(mapSize);
        ResourceFactory.OnAwake(mapSize);
        BuildingFactory.OnAwake();
        //RoomFactory.OnAwake();
        EventFactory.OnAwake();
        UnderBlackMask.OnAwake("UnderGroundGrid");
        UpBlackMask.OnAwake("GroundGrid");

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
