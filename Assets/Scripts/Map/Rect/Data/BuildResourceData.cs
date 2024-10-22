using UnityEngine;

[CreateAssetMenu(fileName ="BuildResourceData", menuName = "Scriptable Object/BuildResource")]
public class BuildResourceData : ScriptableObject
{
    #region Attribute
    [SerializeField]
    string buildingName;
    [SerializeField]
    int[] leaf = { };
    [SerializeField]
    int[] wood = { };
    #endregion

    #region Function
    public string BuildingName { get { return buildingName; } }
    public int[] Leaf { get { return leaf; } }
    public int[] Wood { get { return wood; } }
    #endregion

}
