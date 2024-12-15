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
        }
    }
    public override bool EventFunction(GameObject traveler)
    {
        ChooseResource(true);
        return true;
    }
    #endregion
}
