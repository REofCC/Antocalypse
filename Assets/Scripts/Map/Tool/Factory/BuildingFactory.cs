using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<BuildData> buildResources = new();
    #endregion
    #region Function
    private BuildData GetBuildData(string buildingName)
    {
        BuildingType buildingType = (BuildingType)Enum.Parse(typeof(BuildingType), buildingName);
        return buildResources[(int)buildingType];
    }
    #region Resource
    private bool UseBuildResource(string buildName, int phase = 0)
    {
        BuildData info = GetBuildData(buildName);
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
    private bool CheckBuilding(RoomNode node, string buildingName)
    {
        BuildData info = GetBuildData(buildingName);
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
    public bool Build(RoomNode node, string buildingName)
    {
        if (!CheckBuilding(node, buildingName))
            return false;
        if (!UseBuildResource(buildingName))
            return false;
        GameObject building = InstantiateBuilding(buildingName);
        if (building == null)
            return false;
        SetBuildingPosition(building, node);
        return true;
    }
    public bool Upgrade(BaseBuilding building)
    {
        int phase = building.GetBuildingLevel();
        if(UseBuildResource(building.GetBuildingType().ToString(), phase))
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
}
