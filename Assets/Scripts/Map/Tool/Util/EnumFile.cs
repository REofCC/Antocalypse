public enum AntType
{
    Worker,
    Scout,
    Soldier,
    Queen,
}
public enum TileType
{
    Wall,
    Path,
    RoomCenter,
    RoomNode,
    DoorNode,
    TravelNode,
    ResourceNode,
    TraveledNode,
    BattleNode,
    None
}
public enum BuildingType
{
    LeafSaver,
    WoodSaver,
    LiquidFoodSaver,
    SolidFoodSaver,
    CombineSaver,
    LargeSaver,
    SolidProcessor,
    ResourceExtractor,
    WorkerSpawner,
    ScoutSpawner,
    SoldierSpawner,
    EvolutionChamber,
    ScoutBaseCamp,
    TravelBaseCamp,
    Queen
}
public enum ResourceType
{
    LEAF,
    WOOD,
    LIQUID_FOOD,
    SOLID_FOOD,
    GENETIC_MATERIAL
}

public enum ResourceSize
{
    Large,
    Medium,
    Small
}
public enum EventType 
{
    Question,
    Battle,
    Resource,

}

public enum LayerType
{
    Ground,
    Underground
}