using System.Collections.Generic;
using UnityEngine;

public class ResourceFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<ResourceData> datas;
    #endregion

    #region Function
    private bool CheckResource(HexaMapNode node, float distance,float cellSize)
    {
        int radio;
        if (node.GetTileType() != TileType.Wall)
            return false;

        if (distance < cellSize * 5)
            radio = 2;
        else
            radio = 4;

        Collider[] cols = Physics.OverlapSphere(node.GetWorldPos(), cellSize*radio, 1<<7);
        if (cols.Length == 0)
        {
            return true;
        }
        return false;
    }
    private ResourceData ResourceSelect(HexaMapNode node, float distance, float cellSize)
    {
        if(distance < cellSize * 5)
            return datas[UnityEngine.Random.Range(0, 3)];
        else if (distance < cellSize * 10)
            return datas[UnityEngine.Random.Range(3, 6)];
        else 
            return datas[UnityEngine.Random.Range(6, 9)];    
    }
    private GameObject InstantiateResource(string name)
    {
        GameObject resource = Resources.Load<GameObject>($"Prefabs/Resource/{name}");
        if (resource == null)
        {
            Debug.Log($"Error : Prefabs/Resource/{name} is not exist");
            return null;
        }
        return Instantiate(resource);
    }
    private void SetResource(BaseResource resource, HexaMapNode node, ResourceData data)
    {
        resource.SetResourceData(data);
        node.SetBuildable(data.Buildable);
        node.SetBreakable(false);
        node.SetWalkable(false);
    }
    public void MakeResource(HexaMapNode node, float distance, float cellSize)
    {
        if (!CheckResource(node, distance, cellSize))
            return;
        ResourceData info = ResourceSelect(node, distance, cellSize);

        GameObject obj = InstantiateResource(info.ResourceName);
        if(obj == null)
        {
            return;
        }

        SetResource(obj.GetComponent<BaseResource>(), node, info);
        obj.transform.position = node.GetWorldPos();
    }
    #endregion
}