using System.Collections.Generic;
using UnityEngine;

public class SaverPath : MonoBehaviour 
{
    #region Attribute
    [SerializeField]
    List<Resourcetype> types;
    HexaMapNode node;
    List<Vector3> route;
    #endregion
    #region Function
    private void ComparePath(BaseResource resource)
    {
        this.route = resource.GetRoute();
        List<Vector3> route = MapManager.Map.UnderPathFinder.PathFinding(resource.GetNode(), node);
        if (this.route == null || route != null && this.route.Count > route.Count)
        {
            this.route = route;
        }
        resource.SetRoute(route);
        this.route = null;
    }
    private void SetResourcesPath(List<BaseResource> resources)
    {
        foreach (BaseResource resource in resources)
        {
            ComparePath(resource);
        }
    }
    public void SetRoute()
    {
        for (int i = 0; i < types.Count; i++) 
        {
            List<BaseResource> res = Managers.Resource.resGroup.GetResources(types[i]);
            SetResourcesPath(res);
        }
    }

    #endregion
    #region UnityFunction
    public void Start()
    {
        node = this.gameObject.GetComponent<BaseBuilding>().GetBuildedPos();
        SetRoute();
    }
    #endregion
}
