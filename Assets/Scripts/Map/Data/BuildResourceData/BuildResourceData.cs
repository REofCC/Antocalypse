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
    #endregion

    #region Function
    public string BuildingName { get { return buildingName; } }
    public int[] Leaf { get { return leaf; } }
    public int[] Wood { get { return wood; } }
    #endregion

}
