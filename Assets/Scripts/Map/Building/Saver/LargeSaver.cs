public class LargeSaver : BaseBuilding
{
    int amount = 150;
    public override void EventStart()
    {
        Managers.Resource.CalcMaxSolidFood(amount);
    }

    public override void EventStop()
    {
        Managers.Resource.CalcMaxSolidFood(amount * -1);
    }
}