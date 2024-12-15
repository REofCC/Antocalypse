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
    Queen,
    None
}
public enum ResourceType
{
    LEAF,
    WOOD,
    LIQUID_FOOD,
    SOLID_FOOD,
    GENETIC_MATERIAL
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

public enum BuffType
{
    HoldValue,  //운반 가능한 자원 상한
    MoveSpeed,  //이동 속도
    PopulationCap,  //부화지가 제공하는 상한
    SpawnCost,  //부화 비용
    GatherSpeed,    //채집 속도
    BuildSpeed, //건설,파괴,땅 파기 속도
    FoodConsume,    //식량 소모
    ScoutSight, //지상 시야
    MinScout,   //최소 탐사 인원
    CombatPower,    //전투력
    CombatReward,   //전투 승리 보상
    CombatLoss, //개체 손실 감소
    None    //마지막
}