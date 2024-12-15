using System.Collections.Generic;
using UnityEngine;

public class SaverPath : MonoBehaviour 
{
    #region Attribute
    [SerializeField]
    HexaMapNode node;
    List<Vector3> route;
    
    [SerializeField]
    SaveData saveInfo;
    
    //List<int> currentSave = new ();

    #endregion
    #region Getter
    /*
    public int GetCurrentSave(ResourceType type)
    {
        return currentSave[(int)type];
    }
    */
    public List<ResourceType> GetSaveResourceType()
    {
        return saveInfo.SaveResources;
    }
    #endregion
    #region Function
    #region PathFind
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
        List<ResourceType> types = saveInfo.SaveResources;
        for (int i = 0; i < types.Count; i++) 
        {
            List<BaseResource> res = Managers.Resource.resGroup.GetResources(types[i]);
            SetResourcesPath(res);
        }
    }
    #endregion
    #region Resource Save
    /*
    private void InitCurrentSave()
    {
        for (int idx = 0; idx < 4; idx++)
        {
            currentSave.Add(0);
        }
    }
    */
    /*
    public bool CheckSaveState(int value, ResourceType type)
    {
        if (!CheckResourceType(type))
            return false;
        int diff = saveInfo.MaxSave[(int)type] - currentSave[(int)type];
        if (diff < value)
        {
            return false;
        }
        return true;
    }
    */
    public bool CheckResourceType(ResourceType type)
    {
        for (int i = 0; i < saveInfo.SaveResources.Count; i++)
        {
            if (type == saveInfo.SaveResources[i])
                return true;
        }
        return false;
    }
    /*
    public int GatherResource(int value, ResourceType type)
    {
        int amount = value;
        int diff = saveInfo.MaxSave[(int)type] - currentSave[(int)type];
        if (amount < diff)
        {
            amount = diff;
        }
        currentSave[(int)type] += amount;
        return value - amount;
    }
    */
    public void GatherResource(int value, ResourceType type)
    {
        Managers.Resource.AddResource(value, type);
    }
    #endregion
    #endregion
    #region UnityFunction
    public void Start()
    {
        node = this.gameObject.GetComponent<BaseBuilding>().GetBuildedPos();
        //SetRoute();
        //InitCurrentSave();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Worker worker = collision.GetComponent<Worker>();
        //TODO Gather Resource
        /*
        if (CheckResource(value, type))
        {
            GatherResource(value, type);
        }
        */
    }
    #endregion
}
