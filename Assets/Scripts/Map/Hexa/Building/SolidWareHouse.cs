using UnityEngine;

public class SolidWareHouse : BaseBuilding
{
    #region Attribute
    int[] solidSaveInc = { };
    #endregion

    #region Getter & Setter
    private int GetSolidSaveInc(int level)
    {
        return solidSaveInc[level];
    }
    #endregion

    #region Function

    public override bool UpgradeBuilding()
    {
        int value = GetSolidSaveInc(GetBuildingLevel()+1) - GetSolidSaveInc(GetBuildingLevel());
        SetBuildingLevel(GetBuildingLevel() + 1);
        GameState.Instance.CalcMaxSolidFood(value);
        return true;
    }
    #endregion
}