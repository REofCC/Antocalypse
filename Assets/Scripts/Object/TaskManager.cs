using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager
{
    List<GameObject> workers = new List<GameObject>();

    GameObject entity;
    Vector2 resourceNodePos;
    LayerMask antLayer;
    public void OnStart()
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
            entity.GetComponent<Worker>().GetTask(resourceNodePos, TaskType.Gather);
        }
        else
        {
            Debug.Log("No Entity to Order");
        }
    }

    public GameObject FindEntity(TaskType task)
    {
        GameObject entity = null;

        foreach (var hit in Physics2D.CircleCastAll(resourceNodePos, Mathf.Infinity, Vector2.zero,Mathf.Infinity, antLayer))
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
    public GameObject FindEntity(TaskType task, Vector2 nodePos)
    {
        GameObject entity = null;

        foreach (var hit in Physics2D.CircleCastAll(resourceNodePos, Mathf.Infinity, Vector2.zero, Mathf.Infinity, antLayer))
        {
            if ((hit.collider.GetComponent<Worker>().GetCurrentTask() == task))
            {
                entity = hit.collider.gameObject;
                if (entity.GetComponent<Worker>().GetTargetNodePos() == nodePos)
                {
                    Debug.Log("Found");
                    break;
                }
            }
        }

        if (entity == null)
        {
            Debug.Log("Can't Find");
        }

        return entity;
    }

    public void AssignTask(TaskType task, GameObject node)
    {
        resourceNodePos = node.transform.position;
        entity = FindEntity(TaskType.None);

        if (!entity)
        {
            Debug.Log("There's No Idle Entity");
            return;
        }
        node.GetComponent<ResourceNode>().ChangeCurrentWorker(1);
        entity.GetComponent<Worker>().GetTask(resourceNodePos, task);
    }
    public void DismissTask(TaskType task, GameObject node)
    {
        entity = FindEntity(task, node.transform.position);

        if (!entity)
        {
            Debug.Log("There's No Gathering Entity");
            return;
        }
        node.GetComponent<ResourceNode>().ChangeCurrentWorker(-1);
        entity.GetComponent<Worker>().GetTask(TaskType.None);
    }
}
