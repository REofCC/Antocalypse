using System;

public class PopulationManager
{
    //[LSH:REVIEW]: Setter에 이벤트 연결 해주세요
    #region Event    
    public event Action<AntType> OnPopulationChange;
    #endregion
    #region Attribute
    int spawnResourceDecs;
    float spawnDescPerc;
    int maxPopulation;
    // 권희준 - 병종 별 인구
    int currentWorker;
    int maxWorker;
    int currentScout;
    int maxScout; 
    int currentSoldier;
    int maxSoldier;
    #endregion
    #region Getter & Setter
    public float GetSpawnDesc()
    {
        return spawnDescPerc;
    }
    // 권희준 - 병종별 인구 추가에 따른 함수 변경 및 추가
    public int GetCurrnetPopulation(AntType type)
    {
        switch (type)
        {
            case AntType.Worker:
                return currentWorker;
            case AntType.Scout:
                return currentScout;
            case AntType.Soldier:
                return currentSoldier;
        }
        return -1;
    }
    public int GetMaxPopulation(AntType type)
    {
        switch (type)
        {
            case AntType.Worker:
                return maxWorker;
            case AntType.Scout:
                return maxScout;
            case AntType.Soldier:
                return maxSoldier;
        }
        return -1;
    }

    #endregion
    #region Function
    public void CalcSpawnResourceDesc(int value)
    {
        spawnResourceDecs += value;
        spawnDescPerc = (spawnResourceDecs)/(spawnResourceDecs+100);
    }
    // 권희준 - 병종 별 현재/최대 인구 변동
    public void CalcMaxPopulation(AntType type, int value)
    {
        switch (type)
        {
            case AntType.Worker:
                if (maxWorker + value < 0)
                {
                    UnityEngine.Debug.LogError("Can't Reduce MaxCap Below 0");
                    return;
                }
                maxWorker += value;
                break;
            case AntType.Scout:
                if (maxScout + value < 0)
                {
                    UnityEngine.Debug.LogError("Can't Reduce MaxCap Below 0");
                    return;
                }
                maxScout += value;
                break;
            case AntType.Soldier:
                if (maxSoldier + value < 0)
                {
                    UnityEngine.Debug.LogError("Can't Reduce MaxCap Below 0");
                    return;
                }
                maxSoldier += value;
                break;
        }
    }
    public void CalcCurrentPopulation(AntType type, int value)
    {
        switch (type)
        {
            case AntType.Worker:
                if (currentWorker + value > maxWorker)
                {
                    UnityEngine.Debug.LogError("Reached MaxCap");
                    return;
                }
                currentWorker += value;
                break;
            case AntType.Scout:
                if (currentScout + value > maxScout)
                {
                    UnityEngine.Debug.LogError("Reached MaxCap");
                    return;
                }
                currentScout += value;
                break;
            case AntType.Soldier:
                if (currentSoldier + value > maxSoldier)
                {
                    UnityEngine.Debug.LogError("Reached MaxCap");
                    return;
                }
                currentSoldier += value;
                break;
        }
    }
    #endregion
    // 권희준 - Init
    public void Init()
    {
        currentWorker = 1;
        maxWorker = 3;
    }
}
