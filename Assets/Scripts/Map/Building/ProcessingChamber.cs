using JetBrains.Annotations;
using UnityEngine;

public class ProcessingChamber : BaseBuilding
{
    #region Attribute
    float[] time = { };
    int[] MaxChangeFood = { };
    float ratio = 1;
    #endregion

    #region Getter & Setter
    private float GetTime()
    {
        return time[GetBuildingLevel()];
    }
    private int GetMaxChange()
    {
        return MaxChangeFood[GetBuildingLevel()];
    }
    #endregion

    #region Function
    public int CalcRelease(int solid)
    {
        return (int)(solid * ratio);
    }
    public bool ChangeFood(int solid)
    {
        if(solid > GetMaxChange() || solid <= 0)
            return false;
        return Managers.Resource.ChangeFood(solid, CalcRelease(solid), GetTime(), ratio);
    }
    public override bool UpgradeBuilding()
    {
        SetBuildingLevel(GetBuildingLevel() + 1);
        return true;
    }
    #endregion
}