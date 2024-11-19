using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour
{
    [SerializeField]
    protected NodeData nodeData;
    CircleCollider2D circleCollider;

    private string nodeType;
    private int maxWorker;
    private int currnetWorker;
    
    public void AssinBuild()
    {
        GameManager.Task.AssignTask(TaskType.Build, gameObject);
    }
    public void ChangeCurrentWorker(int value)
    {
        currnetWorker += value;
    }
}
