public class SoldierSpawner : BaseBuilding
{
    public override void EventStart()
    {
        Managers.Population.CalcMaxPopulation(AntType.Soldier, 5);
    }

    public override void EventStop()
    {
        Managers.Population.CalcMaxPopulation(AntType.Soldier, -5);
    }
}
