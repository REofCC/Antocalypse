using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaTileDict : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    Dictionary<string, Func<HexaMapNode>> tileDict = new()
    {
        { "Walkable Tile", ()=>new HexaMapNode() },
        { "Unwalkable Tile", ()=>new HexaMapNode() }
    };
    #endregion
    public int GetCount()
    {
        return tileDict.Count;
    }

    public HexaMapNode GetNode(string key)
    {
        return tileDict[key]();
    }
}
