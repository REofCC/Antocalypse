using UnityEngine;

public class QuestionEvent : Event
{
    public override bool EventFunction(GameObject traveler)
    {
        MapManager.Map.EventFactory.GenerateEvent(GetNode());
        return true;
    }

    public override void OnComplete()
    {
        Destroy(this.gameObject);
        return;
    }

    public override void SetEvent()
    {
        SetEventType(EventType.Question);
    }
}
