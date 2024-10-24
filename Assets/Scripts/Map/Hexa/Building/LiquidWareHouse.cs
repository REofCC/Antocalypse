using UnityEngine;

public class LiquidWareHouse : BaseBuilding
{
    #region Attribute
    int[] liquidSaveInc = { };
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
        GameState.Instance.CalcMaxLiquidFood(value);
        return true;
    }
    #endregion
}