using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : HexaMapNode
{
    public override void Start()
    {
        SetBreakable(true);
        SetWalkable(false);
        SetBuildable(false);
        SetTileType(TileType.Wall);
    }
}
