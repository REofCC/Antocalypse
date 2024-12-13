public class SolidFoodProcssor : BaseBuilding
{
    float ratio = 1;
    float time = 10;
    public override void EventStart()
    {
        return;
    }

    public override void EventStop()
    {
        return;
    }

    public void ChangeFood(int value)
    {
        Managers.Resource.ChangeFood(value, time, ratio);
    }
}
