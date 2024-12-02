using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TaskManager
{
    List<GameObject> workers = new List<GameObject>();

    GameObject entity;
    Vector2 nodePos;
    LayerMask antLayer;

    float scanRange;
    public void Init()
    {
        scanRange = 100f;
        antLayer = 1 << LayerMask.NameToLayer("Ant");

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
            //entity.GetComponent<Worker>().GetTask(nodePos, TaskType.Gather);
        }
        else
        {
            Debug.Log("No Entity to Order");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(nodePos, scanRange);
    }
    public GameObject FindEntity(TaskType task)
    {
        GameObject entity = null;
         
        var hits = Physics2D.CircleCastAll(nodePos, scanRange, Vector2.zero, Mathf.Infinity, antLayer);

        foreach (var hit in hits.OrderBy(distance => Vector2.Distance(nodePos, distance.point)))
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

        foreach (var hit in Physics2D.CircleCastAll(nodePos, Mathf.Infinity, Vector2.zero, Mathf.Infinity, antLayer))
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

    public void AssignTask(TaskType task, HexaMapNode targetNode)
    {
        entity = FindEntity(TaskType.None);

        if (!entity)
        {
            Debug.Log("There's No Idle Entity");
            return;
        }

        nodePos = targetNode.GetWorldPos();
        Vector2Int gridPos = targetNode.GetGridPos();

        HexaMapNode start = MapManager.Map.UnderGrid.GetNode(entity.transform.position);
        //List<Vector3> path = MapManager.Map.PathFinder.PathFinding(start, targetNode);
        List<Vector3> path = MapManager.Map.PathFinder.ReachWallPathFinding(start, targetNode); //대상이 벽일 경우 이전 노드까지 탐색
         
        switch (task)
        {
            case TaskType.Gather:
                //target.GetComponent<ResourceNode>().ChangeCurrentWorker(1);
                entity.GetComponent<Worker>().GetTask(targetNode, path, task);
                break;
            case TaskType.Build:
                entity.GetComponent<Worker>().GetTask(targetNode, path, task);
                break;
        }
        
    }
    public void DismissTask(TaskType task, GameObject node)
    {
        entity = FindEntity(task, node.transform.position);

        if (!entity)
        {
            Debug.Log("There's No Working Entity");
            return;
        }

        switch (task)
        {
            case TaskType.Gather:
                node.GetComponent<ResourceNode>().ChangeCurrentWorker(-1);
                entity.GetComponent<Worker>().GetTask(TaskType.None);
                break;
            case TaskType.Build:
                //node.GetComponent<ResourceNode>().ChangeCurrentWorker(-1);
                entity.GetComponent<Worker>().GetTask(TaskType.None);
                break;
        }

    }
}
