using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode2 : HexaMapNode
{
    #region Attribute
    BaseResource resource;
    #endregion

    #region Getter & Setter
    public void SetResource(BaseResource resource)
    {
        this.resource = resource;
    }
    public BaseResource GetResource()
    {
        return resource;
    }
    #endregion

    public override void Start()
    {
        SetBreakable(false);
        SetWalkable(true);
        SetBuildable(false);
        SetTileType(TileType.ResourceNode);
    }
}
