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
    public override bool EventFunction(GameObject traveler)
    {
        GetResource(GetResourceInfo());
        return true;
    }
    #endregion
}
