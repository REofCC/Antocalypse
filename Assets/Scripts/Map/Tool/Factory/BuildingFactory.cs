using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<BuildData> buildResources = new();
    #endregion
    #region Getter
    public List<BuildData> GetBuildDatas() 
    {
        return buildResources; 
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
    private bool CheckBuilding(RoomNode node, BuildData info)
    {
        bool buildable = false;
        for(int i = 0; i < info.Tiles.Count; i++)
        {
            if (info.Tiles[i] == node.GetTileType())
                buildable = true;
        }
        if (buildable)
        {
            return node.GetBuildable();
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
        return Instantiate(go);
    }
    private void SetBuildingPosition(GameObject building, RoomNode node)
    {
        building.transform.position = node.GetWorldPos();
        building.GetComponent<BaseBuilding>().SetBuildedPos(node);
        node.SetBuilding(building.GetComponent<BaseBuilding>());

    }
    private bool BuildStart(RoomNode node, BuildData info)
    {
        if (!CheckBuilding(node, info))
            return false;
        if (!UseBuildResource(info))
            return false;
        return true;
    }
    private bool BuildEnd(RoomNode node, BuildData info)
    {
        GameObject building = InstantiateBuilding(info.BuildingName);
        if (building == null)
            return false;
        SetBuildingPosition(building, node);
        return true;
    }
    public void Build(RoomNode node, BuildingType type)
    {
        BuildData info = GetBuildData(type);
        StartCoroutine(BuildingCoroutine(node, info));
    }
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
    #endregion
    #region Demolition
    public bool Demolition(GameObject building)
    {
        if (building == null) return false;
        BaseBuilding info = building.GetComponent<BaseBuilding>();
        if (info == null) return false;

        RoomNode pos = (RoomNode)info.GetBuildedPos();
        pos.Demolition();

        Destroy(building);
        return true;
    }
    #endregion
    #endregion
    #region Coroutine Time
    IEnumerator BuildingCoroutine(RoomNode node, BuildData info)
    {
        if(!BuildStart(node, info))
        {
            yield break;
        }
        yield return new WaitForSeconds(info.Time);
        BuildEnd(node, info);
        yield break;
    }
    #endregion
}
