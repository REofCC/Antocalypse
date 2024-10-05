using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeFactory : MonoBehaviour
{
    #region Attribute
    Dictionary<string, Func<HexaMapNode>> nodeDict = new()
    {
        { "WalkableTile", ()=>new HexaMapNode() },
        { "UnwalkableTile", ()=>new HexaMapNode() }
    };
    [SerializeField]
    List<TileBase> tileDict = new();
    #endregion
    public int GetCount()
    {
        return nodeDict.Count;
    }

    public HexaMapNode GetNode(string key)
    {
        return nodeDict[key]();
    }

    public TileBase GetTile(int idx)
    {
        return tileDict[idx];
    }
}
