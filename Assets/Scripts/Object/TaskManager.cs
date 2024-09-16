using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    GameObject resourceNode;
    [SerializeField]
    GameObject[] workers; 
    public void ResourceCollect()
    {
        Debug.Log("Order Collect");
        workers[0].GetComponent<Worker>().MoveToward(resourceNode.transform.position, TaskType.Gather);
    }

}
