public class WorkerSpawner : BaseBuilding
{
    public override void EventStart()
    {
        Managers.Population.CalcMaxPopulation(AntType.Worker, 5);
    }

    public override void EventStop()
    {
        Managers.Population.CalcMaxPopulation(AntType.Worker, -5);
    }
}
