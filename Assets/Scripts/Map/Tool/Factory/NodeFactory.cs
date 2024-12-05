using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeFactory : MonoBehaviour
{
    #region Attribute
    readonly Dictionary<string, Func<HexaMapNode>> nodeDict = new()
    {
        { "Wall", ()=>new Wall() },
        { "Path", ()=>new Path() },
        { "RoomCenter", ()=>new RoomCenter() },
        { "RoomNode", ()=>new RoomNode() },
        {"DoorNode", ()=>new DoorNode() },
        { "TravelNode", ()=>new TravelNode()}
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
