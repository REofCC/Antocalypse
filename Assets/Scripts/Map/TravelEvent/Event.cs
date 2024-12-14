using UnityEngine;

public abstract class Event : MonoBehaviour
{
    #region Attribute
    EventType eventType;
    TravelNode node;
    [SerializeField]
    EventData info;
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
    public void SetEventData(EventData info)
    {
        this.info = info;
    }
    public EventData GetResourceInfo()
    {
        return info;
    }
    #endregion
    #region Function
    public void GetResource(EventData info)
    {
        Managers.Resource.AddLiquidFood(info.DropLiquid);
        Managers.Resource.AddSolidFood(info.DropSolid);
        Managers.Resource.AddWood(info.DropWood);
        Managers.Resource.AddWood(info.DropLeaf);
    }
    public abstract void SetEvent(EventData data);
    public abstract bool EventFunction(GameObject traveler);
    public virtual void OnComplete()
    {
        node.OnEventComplete();
        Destroy(this.gameObject);
    }
    #endregion
    #region UnityFunction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EventFunction(collision.gameObject))
        {
            OnComplete();
        }
    }
    #endregion
}
