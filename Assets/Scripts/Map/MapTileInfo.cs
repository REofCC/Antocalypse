using UnityEngine;

[CreateAssetMenu(fileName = "MapTileInfo", menuName ="Scriptable Object/ MapTileInfo")]
public class MapTileInfo : ScriptableObject
{
    #region attribute
    [SerializeField]
    private bool walkable;
    [SerializeField]
    private string tileName;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int layer;
    #endregion

    #region Getter & Setter
    public bool Walkable { get { return walkable; }}
    public string TileName { get { return tileName; } }
    public Sprite Sprite { get { return sprite; }}
    public int Layer { get { return layer; }}
    #endregion
}