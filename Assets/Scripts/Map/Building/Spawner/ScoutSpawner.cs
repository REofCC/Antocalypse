public class ScoutSpawner : BaseBuilding
{
    public override void EventStart()
    {
        Managers.Population.CalcMaxPopulation(AntType.Scout, 5);
    }

    public override void EventStop()
    {
        Managers.Population.CalcMaxPopulation(AntType.Scout, -5);
    }
}
