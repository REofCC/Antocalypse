public enum TileType
{
    Wall,
    Path,
    RoomCenter,
    RoomNode,
    DoorNode,
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
    Battle,
    Resource,

}