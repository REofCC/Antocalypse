public class CombineSaver : BaseBuilding
{
    int amount = 50;
    public override void EventStart()
    {
        Managers.Resource.CalcMaxWood(amount);
        Managers.Resource.CalcMaxLeaf(amount);
        Managers.Resource.CalcMaxSolidFood(amount);
        Managers.Resource.CalcMaxLiquidFood(amount);
    }

    public override void EventStop()
    {
        Managers.Resource.CalcMaxWood(amount * -1);
        Managers.Resource.CalcMaxLeaf(amount * -1);
        Managers.Resource.CalcMaxSolidFood(amount * -1);
        Managers.Resource.CalcMaxLiquidFood(amount * -1);
    }
}
