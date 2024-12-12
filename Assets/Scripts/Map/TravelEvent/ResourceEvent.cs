using UnityEngine;

public class ResourceEvent : Event
{
    #region Attribute
    #endregion
    #region Setter & Getter

    #endregion
    #region Function
    public override void SetEvent()
    {
        SetEventType(EventType.Resource);
    }
    public override bool EventFunction(GameObject traveler)
    {
        GetResource();
        return true;
    }
    #endregion
}
