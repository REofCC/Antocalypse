using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : HexaMapNode
{
    BaseResource resource;

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
        SetBreakable(true);
        SetWalkable(false);
        SetBuildable(false);
        SetTileType(TileType.Wall);
    }
}
