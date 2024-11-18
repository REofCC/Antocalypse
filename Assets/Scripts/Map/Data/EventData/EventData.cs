using System.Collections;
using System.Collections.Generic;
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

    #endregion

    #region Getter & Setter
    public string EventName {  get { return eventName; } }
    public int DropLiquid { get { return dropLiquid; } }
    public int DropSolid { get { return dropSolid; } }
    #endregion
}
