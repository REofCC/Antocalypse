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
        int idx = Random.Range(1, eventDict.Count);
        return eventDict[idx];
    }
    private bool CheckNode(HexaMapNode node)
    {
        if (node == null ||node.GetTileType() != TileType.TravelNode)
            return false;
        return true;
    }
    private GameObject GenerateEventObject(EventData eventData)
    {
        GameObject obj = Resources.Load<GameObject>($"Prefabs/Event/{eventData.name}");
        if (obj == null)
        {
            Debug.Log($"Error : Prefabs/Event/{eventData.name} is not exist");
            return null;
        }
        return Instantiate(obj, eventObjects.transform);
    }
    private void CombineEventNode(GameObject obj, TravelNode node, EventData data)
    {
        obj.transform.position = node.GetWorldPos();
        obj.GetComponent<Event>().SetNode(node);
        obj.GetComponent<Event>().SetEvent(data);
        node.SetEvent(obj.GetComponent<Event>());
    }
    public void GenerateEvent(TravelNode node)
    {
        CheckNode(node);
        if (node == null)
        {
            return;
        }
        EventData data = SelectEvent();
        GameObject obj = GenerateEventObject(data);
        CombineEventNode(obj, node, data);
    }

    public void GenerateEvent(TravelNode node, int idx)
    {
        CheckNode(node);
        if (node == null)
        {
            return;
        }
        EventData data = eventDict[idx];
        GameObject obj = GenerateEventObject(data);
        CombineEventNode(obj, node, data);
    }
    #endregion
    public void OnAwake()
    {
        eventObjects = GameObject.Find("Events");
    }
}
