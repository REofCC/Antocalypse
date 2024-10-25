using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField]
    protected NodeData nodeData;
    CircleCollider2D circleCollider;

    private string nodeType;
    private int value;
    private int maxWorker;
    private int currnetWorker;

    void Start()
    {
        InitData();
    }
    private void InitData()
    {
        nodeType = nodeData.nodeType;
        value = nodeData.value;
        maxWorker = nodeData.maxWorker;
        currnetWorker = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Worker"))
        {
            if (collision.gameObject.GetComponent<Worker>().GetCurrentState() == State.Gather) //자원 수집 개미와 충돌 시
            {
                Debug.Log("Gather Resource");
                nodeData.value -= collision.gameObject.GetComponent<Worker>().GetGatherValue();
            }
        }
    }
    
    public void AssinGather()
    {
        if (currnetWorker >= maxWorker)
        {
            Debug.Log("Too Many Workers");
            return;
        }
        GameManager.Task.AssignTask(TaskType.Gather, gameObject);
    }
    public void DismissGather()
    {
        if (currnetWorker == 0)
        {
            Debug.Log("There Are No Working Workers");
            return;
        }
        GameManager.Task.DismissTask(TaskType.Gather, gameObject);
    }
    public void ChangeCurrentWorker(int value)
    {
        currnetWorker += value;
    }
}
