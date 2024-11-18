using UnityEngine;

public class LiquidWareHouse : BaseBuilding
{
    #region Attribute
    int[] liquidSaveInc = { };
    Resourcetype type = Resourcetype.Liquid;
    #endregion

    #region Getter & Setter
    private int GetLiquidSaveInc(int level)
    {
        return liquidSaveInc[level];
    }
    #endregion

    #region Function
    public override bool UpgradeBuilding()
    {
        int value = GetLiquidSaveInc(GetBuildingLevel()+1) - GetLiquidSaveInc(GetBuildingLevel());
        SetBuildingLevel(GetBuildingLevel() + 1);
        Managers.Resource.CalcMaxLiquidFood(value);
        return true;
    }
    #endregion

    #region UnityFunction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Worker worker = collision.gameObject.GetComponent<Worker>();
        if(worker!= null && worker.GetCurrentState() ==State.Return)
        {
            Managers.Resource.AddLiquidFood(worker.GetGatherValue());
        }
    }
    #endregion
}