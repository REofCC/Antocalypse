using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    GameObject resourceNode;
    [SerializeField]
    List<GameObject> workers;

    GameObject entity;

    LayerMask antLayer;
    private void Awake()
    {
        antLayer = LayerMask.GetMask("Ant");
        workers.AddRange(GameObject.FindGameObjectsWithTag("Worker"));
        //resourceNode.AddRange(GameObject.FindGameObjectsWithTag("resourceNode"));
    }
    public void ResourceCollect()
    {
        GameObject entity = null;
        Debug.Log("Order Collect");
        entity = FindEntity(TaskType.None);
        if (entity != null)
        {
            entity.GetComponent<Worker>().GetTask(resourceNode.transform.position, TaskType.Gather);
        }
        else
        {
            Debug.Log("No Entity to Order");
        }
    }

    public GameObject FindEntity(TaskType task)
    {
        float minDistance;
        GameObject entity = null;

        foreach (var hit in Physics2D.CircleCastAll(resourceNode.transform.position, Mathf.Infinity, Vector2.zero,Mathf.Infinity, antLayer))
        {
            if ((hit.collider.GetComponent<Worker>().GetCurrentTask() == task))
            {
                entity = hit.collider.gameObject;
                Debug.Log("Found");
                break;
            }
        }
        
        if (entity == null)
        {
            Debug.Log("Can't Find");
        }

        return entity;
    }

    public void AssignTask(TaskType task, Vector2 target)
    {
        entity = FindEntity(TaskType.None);

        if (!entity)
        {
            Debug.Log("There's No Idle Entity");
            return;
        }
        entity.GetComponent<Worker>().GetTask(task);
    }
    public void DismissTask(TaskType task, Vector2 target)
    {
        entity = FindEntity(task);

        if (!entity)
        {
            Debug.Log("There's No Gathering Entity");
            return;
        }
        entity.GetComponent<Worker>().GetTask(TaskType.None);
    }
}
