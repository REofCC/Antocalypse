using System.Net;
using UnityEngine;

public class ResourceEvent : Event
{
    #region Attribute
    #endregion
    #region Setter & Getter

    #endregion
    #region Function
    public override void SetEvent(EventData data)
    {
        SetEventType(EventType.Resource);
        SetEventData(data);
    }
    public void ChooseResource(bool choose)
    {
        if (choose)
        {
            MapManager.Map.ResourceFactory.MakeResource(GetNode(), GetEventInfo().ResourceDataIdx);
            OnComplete();
        }
    }
    public override bool EventFunction(GameObject traveler)
    {
        return false;
    }
    #endregion
}
