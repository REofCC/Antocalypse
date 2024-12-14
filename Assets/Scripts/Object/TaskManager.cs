using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public void RequestTask(HexaMapNode _targetNode, TaskType _taskType, string _buildingName)
    {
        TaskRequest newTaskRequest = new TaskRequest(_targetNode, _taskType, _buildingName);
        requestQueue.Enqueue(newTaskRequest);
    }
    IEnumerator TryProcessNextRequest()
    {
        isCoroutineRunning = true;

        while (true)
        {
            if ( requestQueue.Count > 0)
            {
                currentRequest = requestQueue.Dequeue();

                entity = FindEntity(TaskType.None);
                if (entity == null)   //���� ��ü ����
                {
                    requestQueue.Enqueue(currentRequest);   //�ٽ� ť�� �ְ� ���� ��� ����
                }
                else
                {
                    nodePos = currentRequest.targetNode.GetWorldPos();
                    Vector2Int gridPos = currentRequest.targetNode.GetGridPos();

                    HexaMapNode start = MapManager.Map.UnderGrid.GetNode(entity.transform.position);
                    switch (currentRequest.taskType)
                    {
                        case TaskType.Gather:
                            break;
                        case TaskType.Build:
                            if (currentRequest.buildingName == null)  //�� �ı�
                            {
                                List<Vector3> path = MapManager.Map.UnderPathFinder.ReachWallPathFinding(start, currentRequest.targetNode); //����� ���� ��� ���� ������ Ž��
                                entity.GetComponent<Worker>().GetTask(currentRequest.targetNode, path, currentRequest.taskType);
                            }
                            else   //�ǹ� �Ǽ�
                            {
                                List<Vector3> path = MapManager.Map.UnderPathFinder.PathFinding(start, currentRequest.targetNode); //����� ���� �ƴ� ��� �ش� ������ Ž��
                                entity.GetComponent<Worker>().GetTask(currentRequest.targetNode, path, currentRequest.taskType);
                            }
                            break;
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

    struct TaskRequest
    {
        public HexaMapNode targetNode;
        public TaskType taskType;
        public string buildingName;

        // public UnityAction<bool> callback;

        public TaskRequest(HexaMapNode _targetNode, TaskType _taskType) // buildingName ���� Build Task �Ҵ� �� �� Ÿ�Ϸ� ��ü
        {
            targetNode = _targetNode;
            taskType = _taskType;
            buildingName = null;
        }
        public TaskRequest(HexaMapNode _targetNode, TaskType _taskType, string _buildingName)
        {
            targetNode = _targetNode;
            taskType = _taskType;
            buildingName = _buildingName;
        }
    }
