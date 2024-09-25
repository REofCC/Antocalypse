using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "MapTileInfo", menuName = "Scriptable Object/ HexaTileInfo")]
public class HexaMapTileInfo : ScriptableObject
{
    #region Attribute
    [SerializeField]
    bool walkable;
    [SerializeField]
    Tile tile;
    [SerializeField]
    string tileName;
    #endregion

    #region Getter & Setter
    public bool Walkable { get { return walkable; } }
    public string TileName { get { return tileName; } }
    public Tile Tile { get { return tile; } }
    #endregion
}
