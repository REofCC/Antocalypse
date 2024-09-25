using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaTileDict
{
    #region Attribute
    Dictionary<string, Func<HexaMapNode>> tileDict = new()
    {
        { "WalkableTile", ()=>new HexaMapNode() },
        { "UnwalkableTile", ()=>new HexaMapNode() }
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
