using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<ResourceData> datas;
    #endregion

    #region Function
    private GameObject InstantiateResource(string name)
    {
        GameObject resource = Resources.Load<GameObject>($"Prefabs/Resource/{name}");
        if (resource == null)
        {
            Debug.Log($"Error : Prefabs/Resource/{name} is not exist");
        }
        return Instantiate(resource);
    }
    private ResourceData ParsingData(string name)
    {
        ResourceType type = (ResourceType)Enum.Parse(typeof(ResourceType), name);
        return datas[(int)type];
    }
    private void SetResource(BaseResource resource, HexaMapNode node)
    {
        ResourceData data = ParsingData(resource.GetName());
        resource.SetResourceData(data);
        node.SetBuildable(data.Buildable);
        node.SetBreakable(false);
        node.SetWalkable(false);
    }
    public void MakeResource(string name, HexaMapNode node)
    {
        GameObject obj = InstantiateResource(name);
        if(obj == null)
        {
            return;
        }
        SetResource(obj.GetComponent<BaseResource>(), node);
        obj.transform.position = node.GetWorldPos();
    }
    #endregion
}