using UnityEngine;

public class SpawningChamber : BaseBuilding
{
    #region Attribute
    int[] resourceDecs = { };
    #endregion

    #region Getter & Setter
    private int GetResourceDecs(int level)
    {
        return resourceDecs[level];
    }
    #endregion

    #region Function

    public override bool UpgradeBuilding()
    {
        int value = GetResourceDecs(GetBuildingLevel()+1) - GetResourceDecs(GetBuildingLevel());
        SetBuildingLevel(GetBuildingLevel() + 1);
        Managers.Population.CalcSpawnResourceDesc(value);
        return true;
    }
    #endregion
}