using UnityEngine;

public abstract class Event : MonoBehaviour
{
    #region Attribute
    EventType eventType;
    TravelNode node;
    #endregion
    #region Setter & Getter
    public EventType GetEventType()
    {
        return eventType;
    }
    public void SetEventType(EventType type)
    {
        eventType = type;
    }
    public void SetNode(TravelNode node)
    {
        this.node = node;
    }
    public TravelNode GetNode()
    {
        return node;
    }
    #endregion
    #region Function
    public abstract void SetEvent();
    public abstract bool EventFunction(GameObject traveler);
    public abstract void OnComplete();
    #endregion
    #region UnityFunction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventFunction(collision.gameObject);
    }
    #endregion
}
