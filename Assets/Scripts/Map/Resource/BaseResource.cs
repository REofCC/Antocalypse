using UnityEngine;

public class BaseResource : MonoBehaviour
{
    #region Attribute
    int currentAmount;
    [SerializeField]
    string resName;
    ResourceType type;
    HexaMapNode node;
    #endregion

    #region Getter & Setter
    public string GetName()
    {
        return resName;
    }
    public ResourceData GetInfo(ResourceData info)
    {
        return info;
    }
    public int GetCurrentAmount()
    {
        return currentAmount;
    }
    private void SetCurrentAmount(int value)
    {
        currentAmount = value;
    }
    private void SetResourceType(ResourceType type)
    {
        this.type = type;
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
    #endregion

    #region Function
    public void SetResourceData(ResourceData data)
    {
        SetCurrentAmount(data.Amount);
        SetResourceType(data.ResourceType);
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
    #endregion

    #region Unity Function
    #endregion
}
