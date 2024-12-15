using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "Scriptable Object/Event")]
public class EventData : ScriptableObject
{
    #region Attribute
    [SerializeField]
    string eventName;
    [SerializeField]
    int dropLiquid;
    [SerializeField]
    int dropSolid;
    [SerializeField]
    int dropLeaf;
    [SerializeField]
    int dropWood;
    [SerializeField]
    string description;
    [SerializeField]
    int resourceDataIdx;

    #endregion

    #region Getter & Setter
    public string EventName {  get { return eventName; } }
    public int DropLiquid { get { return dropLiquid; } }
    public int DropSolid { get { return dropSolid; } }
    public int DropLeaf { get { return dropLeaf; } }
    public int DropWood { get { return dropWood; } }
    public string Description { get { return description; } }
    public int ResourceDataIdx { get { return resourceDataIdx; } }
    #endregion
}
