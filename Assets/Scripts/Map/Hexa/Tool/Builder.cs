using UnityEngine;

public class Builder : MonoBehaviour
{
    Vector3 buildingOffset = new (0, 0, 0);
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

    }
    public bool Build(RoomNode node, string buildingName)
    {
        if (!CheckBuilding(node))
            return false;
        GameObject building = InstantiateBuilding(buildingName);
        if(building == null)
            return false;
        SetBuildingPosition(building, node);
        node.SetBuilding(building.GetComponent<BaseBuilding>());
        return true;
    }
}
