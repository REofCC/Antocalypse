using UnityEngine;

public class QuestionEvent : Event
{
    bool emptyEvent = false;
    private void RandomEmpty()
    {
        int ratio = Random.Range(1, 101);
        if (ratio > 40)
            emptyEvent = true;
    }
    public override bool EventFunction(GameObject traveler)
    {
        RandomEmpty();
        if (emptyEvent)
        {
            GetNode().OnEventComplete();
            return true;
        }
        MapManager.Map.EventFactory.GenerateEvent(GetNode());
        return true;
    }

    public override void OnComplete()
    {
        Destroy(this.gameObject);
        return;
    }

    public override void SetEvent(EventData data)
    {
        SetEventType(EventType.Question);
        SetEventData(data);
    }
}
