using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    Queue<TaskRequest> requestQueue = new Queue<TaskRequest>();
    List<GameObject> workers = new List<GameObject>();

    TaskRequest currentRequest;

    GameObject entity;
    Vector2 nodePos;
    LayerMask antLayer;
    float scanRange;
    bool isProcessing;
    bool isCoroutineRunning;
    public void Init()
    {
        scanRange = 100f;
        antLayer = 1 << LayerMask.NameToLayer("Ant");

        workers.AddRange(GameObject.FindGameObjectsWithTag("Worker"));
        isProcessing = false;
        isCoroutineRunning = false;
        //resourceNode.AddRange(GameObject.FindGameObjectsWithTag("resourceNode"));
    }
    void FixedUpdate()  //Managers���� �ݺ� ȣ������ ��� �ش� �κ� Managers�� �̵� �� MonoBehaviour ��� ����
    {
        //Debug.Log(requestQueue.Count);
        if (requestQueue.Count > 0 && !isCoroutineRunning)
            StartCoroutine(TryProcessNextRequest());
    }

    public void RequestTask(HexaMapNode _targetNode, TaskType _taskType)
    {
        TaskRequest newTaskRequest = new TaskRequest(_targetNode, _taskType);
        requestQueue.Enqueue(newTaskRequest);
    }
    public void RequestTask(HexaMapNode _targetNode, TaskType _taskType, BuildingType _buildingType)
    {
        TaskRequest newTaskRequest = new TaskRequest(_targetNode, _taskType, _buildingType);
        requestQueue.Enqueue(newTaskRequest);
    }
    IEnumerator TryProcessNextRequest()
    {
        isCoroutineRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if ( requestQueue.Count > 0)
            {
                currentRequest = requestQueue.Dequeue();

                if (!FindEntity(TaskType.None))   //���� ��ü ����
                {
                    requestQueue.Enqueue(currentRequest);   //�ٽ� ť�� �ְ� ���� ��� ����
                }
                else
                {
                    HexaMapNode start = MapManager.Map.UnderGrid.GetNode(entity.transform.position);
                    List<Vector3> path = MapManager.Map.UnderPathFinder.ReachWallPathFinding(start, currentRequest.targetNode);
                    if (path == null)   //��� ���� ��
                    {
                        requestQueue.Enqueue(currentRequest);   //�ٽ� ť�� �ְ� ���� ��� ����
                    }
                    else
                    {
                        switch (currentRequest.taskType)
                        {
                            case TaskType.Gather:
                                entity.GetComponent<Worker>().GetTask(currentRequest.targetNode, currentRequest.taskType);
                                currentRequest.targetNode.GetResource().AddWorker(1);
                                break;
                            case TaskType.Build:
                                if (currentRequest.buildingType == BuildingType.None)  //�� �ı�
                                {
                                    entity.GetComponent<Worker>().GetTask(currentRequest.targetNode, currentRequest.taskType);
                                }
                                else   //�ǹ� �Ǽ�
                                {
                                    entity.GetComponent<Worker>().GetTask(currentRequest.targetNode, currentRequest.taskType, currentRequest.buildingType);
                                }
                                break;
                        }
                    }
                 }
            }
            else
            {
                break;
            }
        yield return null;
        }
        isCoroutineRunning = false;
    }
    public bool FindEntity(TaskType task)
    {
        entity = null;

        var hits = Physics2D.CircleCastAll(nodePos, scanRange, Vector2.zero, Mathf.Infinity, antLayer);

        foreach (var hit in hits.OrderBy(distance => Vector2.Distance(nodePos, distance.point)))
        {
            if ((hit.collider.transform.parent.GetComponent<Worker>().GetCurrentTask() == task))
            {
                entity = hit.collider.transform.parent.gameObject;
                break;
            }
        }

        if (entity == null)
        {
            return false;
        }

        return true;
    }
    public GameObject FindEntity(TaskType task, HexaMapNode node)
    {
        GameObject entity = null;
        Vector2 nodePos = node.GetWorldPos();
        foreach (var hit in Physics2D.CircleCastAll(nodePos, Mathf.Infinity, Vector2.zero, Mathf.Infinity, antLayer))
        {
            if ((hit.collider.transform.parent.GetComponent<Worker>().GetCurrentTask() == task))
            {
                entity = hit.collider.transform.parent.gameObject;
                if (entity.GetComponent<Worker>().resourceNode == node.GetResource())
                {
                    break;
                }
            }
        }

        if (entity == null)
        {
            return null;
        }

        return entity;
    }
    //public void ResourceCollect()
    //{
    //    GameObject entity = null;
    //    Debug.Log("Order Collect");
    //    entity = FindEntity(TaskType.None);
    //    if (entity != null)
    //    {
    //        //entity.GetComponent<Worker>().GetTask(nodePos, TaskType.Gather);
    //    }
    //    else
    //    {
    //        Debug.Log("No Entity to Order");
    //    }
    //}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(nodePos, scanRange);
    }


    //public void AssignTask(TaskType task, HexaMapNode targetNode)
    //{
    //    entity = FindEntity(TaskType.None);

    //    if (!entity)
    //    {
    //        Debug.Log("There's No Idle Entity");
    //        return;
    //    }

    //    nodePos = targetNode.GetWorldPos();
    //    Vector2Int gridPos = targetNode.GetGridPos();

    //    HexaMapNode start = MapManager.Map.UnderGrid.GetNode(entity.transform.position);
    //    //List<Vector3> path = MapManager.Map.PathFinder.PathFinding(start, targetNode);
    //    List<Vector3> path = MapManager.Map.PathFinder.ReachWallPathFinding(start, targetNode); //����� ���� ��� ���� ������ Ž��

    //    switch (task)
    //    {
    //        case TaskType.Gather:
    //            //target.GetComponent<ResourceNode>().ChangeCurrentWorker(1);
    //            entity.GetComponent<Worker>().GetTask(targetNode, path, task);
    //            break;
    //        case TaskType.Build:
    //            entity.GetComponent<Worker>().GetTask(targetNode, path, task);
    //            break;
    //    }
    //}
    public void DismissTask(TaskType task, HexaMapNode node)
    {
        entity = FindEntity(task, node);

        if (!entity)
        {
            return;
        }

        switch (task)
        {
            case TaskType.Gather:
                node.GetResource().RemoveWorker(1);
                entity.GetComponent<Worker>().GetTask(TaskType.None);
                break;
            case TaskType.Build:
                //node.GetComponent<ResourceNode>().ChangeCurrentWorker(-1);
                entity.GetComponent<Worker>().GetTask(TaskType.None);
                break;
        }

    }
}
    struct TaskRequest
    {
        public HexaMapNode targetNode;
        public TaskType taskType;
        public BuildingType buildingType;

        // public UnityAction<bool> callback;

        public TaskRequest(HexaMapNode _targetNode, TaskType _taskType) // buildingName ���� Build Task �Ҵ� �� �� Ÿ�Ϸ� ��ü
        {
            targetNode = _targetNode;
            taskType = _taskType;
        buildingType = BuildingType.None;
        }
        public TaskRequest(HexaMapNode _targetNode, TaskType _taskType, BuildingType _buildingType)
        {
            targetNode = _targetNode;
            taskType = _taskType;
            buildingType = _buildingType;
        }
    }
