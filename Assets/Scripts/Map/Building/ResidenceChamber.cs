using UnityEngine;

public class ResidenceChamber : BaseBuilding
{
    #region Attribute
    int[] populationInc = { };
    #endregion

    #region Getter & Setter
    private int GetPopulationInc(int level)
    {
        return populationInc[level];
    }
    #endregion

    #region Function

    public override bool UpgradeBuilding()
    {
        int value = GetPopulationInc(GetBuildingLevel()+1) - GetPopulationInc(GetBuildingLevel());
        SetBuildingLevel(GetBuildingLevel() + 1);
       // Managers.Population.CalcMaxPopulation(value);
        return true;
    }
    #endregion
}