using UnityEngine;

public class EnemyEvent : Event
{
    #region Attribute
    EnemyData enemyInfo;
    float currentPower;
    #endregion
    #region Setter & Getter
    public void SetEnemyInfo(EnemyData enemyInfo)
    {
        this.enemyInfo = enemyInfo;
    }
    public EnemyData GetEnemyInfo()
    {
        return enemyInfo;
    }
    public float GetCurrentPower()
    {
        return currentPower;
    }
    #endregion
    #region Function
    public override void SetEvent()
    {
        SetEventType(EventType.Battle);
        currentPower = enemyInfo.CombatPower;
    }
    public override bool EventFunction(GameObject traveler)
    {
        /*if(attribute < currentPower)
        {
            //TODO Entity Down
            return false;
        }
        else*/
        {
            OnComplete();
            return true;
        }
    }
    public override void OnComplete()
    {
        Managers.Resource.AddLiquidFood(enemyInfo.DropLiquid);
        Managers.Resource.AddSolidFood(enemyInfo.DropSolid);
        GetNode().OnEventComplete();
        Destroy(this.gameObject);
    }
    #endregion
}
