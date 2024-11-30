using System.Collections.Generic;
using UnityEngine;

public class EventFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<EventData> eventDict = new();
    [SerializeField]
    GameObject eventObjects;
    #endregion

    #region Function
    private EventData SelectEvent()
    {
        int idx = Random.Range(0, eventDict.Count);
        return eventDict[idx];
    }

    private GameObject GenerateEventObject(EventData eventData)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefabs/Event/{eventData.EventName}");
        if (obj == null)
        {
            Debug.Log($"Error : Prefabs/Event/{eventData.EventName} is not exist");
            return null;
        }
        return Instantiate(obj, eventObjects.transform);
    }
    private void CombineEventNode(GameObject obj, TravelNode node)
    {
        obj.transform.position = node.GetWorldPos();
        obj.GetComponent<Event>().SetNode(node);
        node.SetEvent(obj.GetComponent<Event>());
    }
    public void GenerateEvent(TravelNode node)
    {
        if (node == null)
        {
            return;
        }
        GameObject obj = GenerateEventObject(SelectEvent());
        CombineEventNode(obj, node);
    }
    #endregion
}
