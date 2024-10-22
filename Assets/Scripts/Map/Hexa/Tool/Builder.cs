using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    #region Attribute
    Vector3 buildingOffset = new (0, 0, 0);
    [SerializeField]
    List<BuildResourceData> buildResources = new();
    #endregion
    #region Function
    private bool CheckBuilding(RoomNode node)
    {
        return node.GetBuildable();
    }
    private GameObject InstantiateBuilding(string buildingName)
    {
        GameObject go = Resources.Load<GameObject>($"Prefabs/Building/{buildingName}");
        if(go == null)
        {
            Debug.Log($"Error : Prefabs/Building/{buildingName} is not exist");
        }
        return Instantiate(go);
    }
    private void SetBuildingPosition(GameObject building, RoomNode node)
    {
        building.transform.position = node.GetWorldPos() + buildingOffset;
        building.GetComponent<BaseBuilding>().SetBuildedPos(node);
        node.SetBuilding(building.GetComponent<BaseBuilding>());

    }
    public bool Build(RoomNode node, string buildingName)
    {
        if (!CheckBuilding(node))
            return false;
        GameObject building = InstantiateBuilding(buildingName);
        if(building == null)
            return false;
        SetBuildingPosition(building, node);
        return true;
    }
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
}
