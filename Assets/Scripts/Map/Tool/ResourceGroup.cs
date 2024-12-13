using System.Collections.Generic;
using UnityEngine;

public class ResourceGroup : MonoBehaviour
{
    public List<BaseResource> leafs = new();
    public List<BaseResource> woods = new();
    public List<BaseResource> liquids = new();
    public List<BaseResource> solids = new();
    public void AddResource(BaseResource resource)
    {
        switch (resource.GetResourceType())
        {
            case Resourcetype.Leaf:
                leafs.Add(resource);
                break;
            case Resourcetype.Wood:
                woods.Add(resource);
                break;
            case Resourcetype.Liquid:
                liquids.Add(resource);
                break;
            case Resourcetype.Solid:
                solids.Add(resource);
                break;
            default:
                break;
        }
    }
    public void RemoveResource(BaseResource resource)
    {
        switch (resource.GetResourceType())
        {
            case Resourcetype.Leaf:
                leafs.Remove(resource);
                break;
            case Resourcetype.Wood:
                woods.Remove(resource);
                break;
            case Resourcetype.Liquid:
                liquids.Remove(resource);
                break;
            case Resourcetype.Solid:
                solids.Remove(resource);
                break;
            default:
                break;
        }
    }

    public List<BaseResource> GetResources(Resourcetype types)
    {
        switch (types)
        {
            case Resourcetype.Leaf:
                return leafs;
            case Resourcetype.Wood:
                return woods;
            case Resourcetype.Liquid:
                return liquids;
            case Resourcetype.Solid:
                return solids;
            default:
                return null;
        }
    }
}
