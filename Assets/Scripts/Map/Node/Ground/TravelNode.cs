using UnityEngine;

public class TravelNode : HexaMapNode
{
    #region Attribute
    Event travelEvent;
    #endregion

    #region Getter & Setter
    public void SetEvent(Event travelEvent)
    {
        this.travelEvent = travelEvent;
    }
    #endregion

    #region Function
    public void OnEventComplete()
    {
        travelEvent = null;
    }
    #endregion
}
