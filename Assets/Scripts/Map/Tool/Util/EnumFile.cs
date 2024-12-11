public enum AntType
{
    Worker,
    Scout,
    Soldier
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
    None
}
public enum BuildingType
{
    //Population
    ResidenceChamber,
    SpawningChamber,
    //Food
    SolidWareHouse,
    LiquidWareHouse,
    ProcessingChamber,
    //Evolution
    EvolutionChamber,
    GenCollectChamber,
    //Travel
    ExpeditionAggregation
}
public enum Resourcetype
{
    Liquid,
    Solid,
    Leaf,
    Wood
}

public enum EventType 
{
    Question,
    Battle,
    Resource,

}