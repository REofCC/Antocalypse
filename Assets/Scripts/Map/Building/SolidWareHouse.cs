using UnityEngine;

public class SolidWareHouse : BaseBuilding
{
    #region Attribute
    int[] solidSaveInc = { };
    Resourcetype type = Resourcetype.Solid;
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
        Managers.Resource.CalcMaxSolidFood(value);
        return true;
    }
    #endregion
    #region UnityFunction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Worker worker = collision.gameObject.GetComponent<Worker>();
        if (worker != null && worker.GetCurrentState() == State.Return)
        {
            Managers.Resource.AddSolidFood(worker.GetGatherValue());
        }
    }
    #endregion
}