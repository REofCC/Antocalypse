using System.Collections.Generic;
using UnityEngine;

public class BaseResource : MonoBehaviour
{
    #region Attribute
    int currentAmount;
    bool is_extractable;
    bool is_ground;

    int currentWorker;
    List<Worker> workers = new();

    [SerializeField]
    string resName;
    ResourceType type;
    ResourceData info;
    HexaMapNode node;
    BaseBuilding warehouse;

    List<Vector3> route;
    #endregion

    #region Getter & Setter
    public string GetName()
    {
        return resName;
    }
    public ResourceData GetInfo()
    {
        return info;
    }
    public int GetCurrentAmount()
    {
        return currentAmount;
    }
    //권희준 currentWorker Getter
    public int GetCurrentWorker()
    {
        return currentWorker;
    }
    public ResourceType GetResourceType()
    {
        return type;
    }
    public void SetNode(HexaMapNode node)
    {
        this.node = node;
    }
    public HexaMapNode GetNode()
    {
        return node;
    }
    public void SetResourceData(ResourceData data)
    {
        info = data;
        currentAmount = data.Amount;
        type = data.ResourceType;
        is_extractable = data.Extractable;
        is_ground = data.IsGround;
    }
    public void SetWareHouse(BaseBuilding warehouse)
    {
        this.warehouse = warehouse;
    }
    public void SetRoute(List<Vector3> route)
    {
        this.route = route;
    }
    public List<Vector3> GetRoute()
    {
        return route;
    }
    public void SetExtractable(bool extractable)
    {
        this.is_extractable = extractable;
    }
    public bool GetExtractable()
    {
        return is_extractable;
    }
    #endregion

    #region Function
    public void MakeInfinite()
    {
        currentAmount = int.MaxValue;
    }
    private int MinusAmount(int value)
    {
        int amount;
        if (currentAmount < value)
        {
            amount = currentAmount;
            currentAmount = 0;
            return amount;
        }
        currentAmount -= value;
        return value;
    }
    public virtual int Extraction(int value)
    {
        int amount = MinusAmount(value) ;
        
        return amount;
    }
    public void AddWorker(int entities)
    {
        int entityNum;
        //권희준 - 일반 자원노드에는 최대 일꾼수 없음, TaskManger와 명령 직접 연동
        //if(currentWorker + entities > info.MaxWorker)
        //{
        //    return;
        //}
        //for (entityNum = 0; entityNum < entities; entityNum++)
        //{
        //    GameObject worker = null;
        //    //GameObject worker = GameManager.Task.AssignTask(TaskType.Gather, this.gameObject);
        //    if (worker == null)
        //    {
        //        break;
        //    }
        //    workers.Add(worker.GetComponent<Worker>());
        //}
        //currentWorker += entityNum;
        currentWorker += entities;
    }
    public void RemoveWorker(int entities)
    {
        //if (entities > currentWorker)
        //    return;
        //List<Worker> delworkers = new();
        //for (int i = 0; i < entities; i++) 
        //{
        //    workers[i].GetTask(TaskType.None);
        //    delworkers.Add(workers[i]);
        //}
        //for (int i = 0; i < entities; i++)
        //{
        //    workers.Remove(delworkers[i]);
        //}
        currentWorker -= entities;
    }
    #endregion

    #region Unity Function
    // 권희준 - Worker.cs에서 자원 감소 호출
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Worker worker = collision.GetComponent<Worker>();
    //    if (worker == null)
    //    {
    //        return;
    //    }

    //    if (workers.Contains(worker))
    //    {
    //        Extraction(worker.GetGatherValue());
    //    }
    //}
    #endregion
}
