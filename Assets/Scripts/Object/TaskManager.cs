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
        entity = FindIdleEntity();
        if (entity != null)
        {
            entity.GetComponent<Worker>().MoveToward(resourceNode.transform.position, TaskType.Gather);
        }
        else
        {
            Debug.Log("No Entity to Order");
        }
    }

    public GameObject FindIdleEntity()
    {
        float minDistance;
        GameObject entity = null;

        foreach (var hit in Physics2D.CircleCastAll(resourceNode.transform.position, Mathf.Infinity, Vector2.zero,Mathf.Infinity, antLayer))
        {
            if (hit.collider.GetComponent<Worker>().GetCurrentState() == State.Idle)
            {
                entity = hit.collider.gameObject;
                Debug.Log("Idle Found");
                break;
            }
        }
        
        if (entity == null)
        {
            Debug.Log("Can't Find Idle");
        }

        return entity;
    }
}
