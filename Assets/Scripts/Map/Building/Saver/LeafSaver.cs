public class LeafSaver : BaseBuilding
{
    int amount = 50;
    public override void EventStart()
    {
        Managers.Resource.CalcMaxLeaf(amount);
    }

    public override void EventStop()
    {
        Managers.Resource.CalcMaxLeaf(amount * -1);
    }
}
