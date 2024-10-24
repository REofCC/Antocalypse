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
        GameState.Instance.CalcSpawnResourceDesc(value);
        return true;
    }
    #endregion
}