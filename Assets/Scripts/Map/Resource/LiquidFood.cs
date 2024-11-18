using UnityEngine;

public class LiquidFood : BaseResource
{
    #region Attribute
    
    #endregion

    #region Function
    public void SetWareHouse(GameObject warehouse)
    {
        if (warehouse.GetComponent<LiquidWareHouse>() != null)
        {
            SetWareHouse(warehouse.GetComponent<LiquidWareHouse>());
        }
    }
    #endregion
}