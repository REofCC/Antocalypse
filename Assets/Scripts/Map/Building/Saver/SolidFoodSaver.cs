public class SolidFoodSaver : BaseBuilding
{
    int amount = 50;
    public override void EventStart()
    {
        Managers.Resource.CalcMaxSolidFood(amount);
    }

    public override void EventStop()
    {
        Managers.Resource.CalcMaxSolidFood(amount * -1);
    }
}
