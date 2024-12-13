public class LiquidFoodSaver : BaseBuilding
{
    int amount = 50;
    public override void EventStart()
    {
        Managers.Resource.CalcMaxLiquidFood(amount);
    }

    public override void EventStop()
    {
        Managers.Resource.CalcMaxLiquidFood(amount * -1);
    }
}
