using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<BuildData> buildResources = new();
    bool[] buildConstraints; //idx = BuildingType | Modify True when Process complete
    int[] currentBuild; // idx = Bulding Type | Entire Map's Buliding Count
    GameObject buildings;

    #endregion
    #region Getter
    public List<BuildData> GetBuildDatas() 
    {
        return buildResources; 
    } 
    
    public int[] GetCurrentBuildArray()
    {
        return currentBuild;
    }
    public int GetCurrentBuild(BuildingType type)
    {
        return currentBuild[(int)type];
    }

    public void SetBuildingConstaint(BuildingType type, bool permission = true)
    {
        buildConstraints[(int)type] = permission;
    }
    public bool GetBuildingConstraint(BuildingType type)
    {
        return buildConstraints[(int)type];
    }
    #endregion
    #region Function
    private BuildData GetBuildData(BuildingType type)
    {
        return buildResources[(int)type];
    }
    #region Resource
    private bool UseBuildResource(BuildData info, int phase = 0)
    {
        int leaf = info.Leaf[phase];
        int wood = info.Wood[phase];
        ResourceManager state = Managers.Resource;
        if (state.CheckLeaf(leaf)&& state.CheckWood(wood))
        {
            state.MinusLeaf(leaf);
            state.MinusWood(wood);
            return true;
        }
        return false;
    }
    #endregion
    #region Build
    private bool CheckResourceBuilding(ResourceNode2 node)
    {
        if (node.GetResource().GetInfo().Buildable && node.GetBuildable())
            return true;
        return false;
    }
    private bool CheckBuilding(Path node, BuildData info)
    {
        if (!GetBuildingConstraint(info.Type))
            return false;
        for(int i = 0; i < info.Tiles.Count; i++)
        {
            if (info.Tiles[i]==TileType.ResourceNode && node.GetTileType() == TileType.ResourceNode)
            {
                return CheckResourceBuilding(node as ResourceNode2);
            }
            else if (info.Tiles[i] == node.GetTileType()) // check buildable node type
                return node.GetBuildable(); // check already building on node
        }
        return false;
    }
    private GameObject InstantiateBuilding(string buildingName)
    {
        GameObject go = Resources.Load<GameObject>($"Prefabs/Building/{buildingName}");
        if (go == null)
        {
            Debug.Log($"Error : Prefabs/Building/{buildingName} is not exist");
        }
        return Instantiate(go, buildings.transform);
    }
    private void SetBuildingPosition(GameObject building, Path node)
    {
        building.transform.position = node.GetWorldPos();
        building.GetComponent<BaseBuilding>().SetBuildedPos(node);
        node.SetBuilding(building.GetComponent<BaseBuilding>());

    }
    private bool BuildStart(Path node, BuildData info)
    {
        if (!CheckBuilding(node, info))
            return false;
        if (!UseBuildResource(info))
            return false;
        return true;
    }
    private bool BuildEnd(Path node, BuildData info)
    {
        GameObject building = InstantiateBuilding(info.name);
        if (building == null)
            return false;
        SetBuildingPosition(building, node);
        return true;
    }
    public void Build(Path node, BuildingType type)
    {
        BuildData info = GetBuildData(type);
        StartCoroutine(BuildingCoroutine(node, info));
    }
    /*
    public bool Upgrade(BaseBuilding building)
    {
        int phase = building.GetBuildingLevel();
        BuildData info = GetBuildData(building.GetBuildingType());
        if(UseBuildResource(info, phase))
        {
            building.UpgradeBuilding();
            return true;
        }
        return false;
    }
    */
    #endregion
    #region Demolition
    public bool Demolition(GameObject building)
    {
        if (building == null) return false;
        BaseBuilding info = building.GetComponent<BaseBuilding>();
        if (info == null) return false;
        currentBuild[(int)info.GetBuildingType()]--;
        RoomNode pos = (RoomNode)info.GetBuildedPos();
        pos.Demolition();

        Destroy(building);
        return true;
    }
    #endregion
    #endregion
    #region Coroutine Time
    IEnumerator BuildingCoroutine(Path node, BuildData info)
    {
        if(!BuildStart(node, info))
        {
            yield break;
        }
        yield return new WaitForSeconds(info.Time);
        BuildEnd(node, info);
        currentBuild[(int)info.Type]++;
        yield break;
    }
    #endregion
    #region Unity Function
    public void OnAwake()
    {
        currentBuild = new int[buildResources.Count];
        buildConstraints = new bool[buildResources.Count];
        for (int i = 0; i < currentBuild.Length; i++)
        {
            currentBuild[i] = 0;
            buildConstraints[i] = false;
        }
        buildConstraints[(int)BuildingType.Queen] = true;
        buildings = GameObject.Find("Buildings");

    }
    #endregion
}
