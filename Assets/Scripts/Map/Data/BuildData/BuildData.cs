using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BuildData", menuName = "Scriptable Object/Build")]
public class BuildData : ScriptableObject
{
    #region Attribute
    [SerializeField]
    string buildingName;
    [SerializeField]
    int[] leaf = { };
    [SerializeField]
    int[] wood = { };
    [SerializeField]
    List<TileType> tiles = new();
    [SerializeField]
    float time;
    #endregion

    #region Function
    public string BuildingName { get { return buildingName; } }
    public int[] Leaf { get { return leaf; } }
    public int[] Wood { get { return wood; } }
    public List<TileType> Tiles { get { return tiles; } }
    public float Time { get { return time; } }
    #endregion

}
