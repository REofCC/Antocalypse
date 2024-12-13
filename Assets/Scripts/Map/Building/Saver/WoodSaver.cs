public class WoodSaver : BaseBuilding
{
    int amount = 50;
    public override void EventStart()
    {
        Managers.Resource.CalcMaxWood(amount);
    }

    public override void EventStop()
    {
        Managers.Resource.CalcMaxWood(amount * -1);
    }
}
