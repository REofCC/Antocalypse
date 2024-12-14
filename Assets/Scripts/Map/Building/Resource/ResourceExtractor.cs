public class ResourceExtractor : BaseBuilding
{
    public override void EventStart()
    {
        ResourceNode2 node = GetBuildedPos() as ResourceNode2;
        BaseResource res = node.GetResource();
        res.MakeInfinite();
        res.SetExtractable(true);
    }

    public override void EventStop()
    {
        throw new System.NotImplementedException();
    }
}
