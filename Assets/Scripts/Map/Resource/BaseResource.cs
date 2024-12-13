using System.Collections.Generic;
using UnityEngine;

public class BaseResource : MonoBehaviour
{
    #region Attribute
    int currentAmount;
    int currentWorker;
    List<Worker> workers = new();

    [SerializeField]
    string resName;
    ResourceType type;
    ResourceData info;
    HexaMapNode node;
    BaseBuilding warehouse;
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
    //±«»Ò¡ÿ currentWorker Getter
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
    }
    public void SetWareHouse(BaseBuilding warehouse)
    {
        this.warehouse = warehouse;
    }

    #endregion

    #region Function

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
        if(currentWorker + entities > info.MaxWorker)
        {
            return;
        }
        for (entityNum = 0; entityNum < entities; entityNum++)
        {
            GameObject worker = null;
            //GameObject worker = GameManager.Task.AssignTask(TaskType.Gather, this.gameObject);
            if (worker == null)
            {
                break;
            }
            workers.Add(worker.GetComponent<Worker>());
        }
        currentWorker += entityNum;
    }
    public void RemoveWorker(int entities)
    {
        if (entities > currentWorker)
            return;
        List<Worker> delworkers = new();
        for (int i = 0; i < entities; i++) 
        {
            workers[i].GetTask(TaskType.None);
            delworkers.Add(workers[i]);
        }
        for (int i = 0; i < entities; i++)
        {
            workers.Remove(delworkers[i]);
        }
        currentWorker -= entities;
    }
    #endregion

    #region Unity Function
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Worker worker = collision.GetComponent<Worker>();
        if (worker == null)
        {
            return;
        }

        if (workers.Contains(worker))
        {
            Extraction(worker.GetGatherValue());
        }
    }
    #endregion
}
