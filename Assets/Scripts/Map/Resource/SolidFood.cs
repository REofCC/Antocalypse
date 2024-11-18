using UnityEngine;

public class SolidFood : BaseResource
{
    #region Attribute
    
    #endregion

    #region Function
    public void SetWareHouse(GameObject warehouse)
    {
        if (warehouse.GetComponent<SolidWareHouse>() != null)
        {
            SetWareHouse(warehouse.GetComponent<SolidWareHouse>());
        }
    }
    #endregion
}