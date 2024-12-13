using System.Collections.Generic;
using UnityEngine;

public class ResourceGroup
{
    public List<BaseResource> leafs = new();
    public List<BaseResource> woods = new();
    public List<BaseResource> liquids = new();
    public List<BaseResource> solids = new();
    public void AddResource(BaseResource resource)
    {
        switch (resource.GetResourceType())
        {
            case ResourceType.LEAF:
                leafs.Add(resource);
                break;
            case ResourceType.WOOD:
                woods.Add(resource);
                break;
            case ResourceType.LIQUID_FOOD:
                liquids.Add(resource);
                break;
            case ResourceType.SOLID_FOOD:
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
            case ResourceType.LEAF:
                leafs.Remove(resource);
                break;
            case ResourceType.WOOD:
                woods.Remove(resource);
                break;
            case ResourceType.LIQUID_FOOD:
                liquids.Remove(resource);
                break;
            case ResourceType.SOLID_FOOD:
                solids.Remove(resource);
                break;
            default:
                break;
        }
    }

    public List<BaseResource> GetResources(ResourceType types)
    {
        switch (types)
        {
            case ResourceType.LEAF:
                return leafs;
            case ResourceType.WOOD:
                return woods;
            case ResourceType.LIQUID_FOOD:
                return liquids;
            case ResourceType.SOLID_FOOD:
                return solids;
            default:
                return null;
        }
    }
}
