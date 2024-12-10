public class PopulationManager
{
    #region Attribute
    int spawnResourceDecs;
    float spawnDescPerc;
    int maxPopulation;
    // ������ - ���� �� �α�
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
    // ������ - ������ �α� �߰��� ���� �Լ� ���� �� �߰�
    public int GetCurrnetPopulation(AntType type)
    {
        switch (type)
        {
            case AntType.Worker:
                return currentWorker;
                break;
            case AntType.Scout:
                return currentScout;
                break;
            case AntType.Soldier:
                return currentSoldier;
                break;
        }
    }
    public int GetMaxPopulation(AntType type)
    {
        switch (type)
        {
            case AntType.Worker:
                return maxWorker;
                break;
            case AntType.Scout:
                return maxScout;
                break;
            case AntType.Soldier:
                return maxSoldier;
                break;
        }
    }

    #endregion
    #region Function
    public void CalcSpawnResourceDesc(int value)
    {
        spawnResourceDecs += value;
        spawnDescPerc = (spawnResourceDecs)/(spawnResourceDecs+100);
    }
    // ������ - ���� �� ����/�ִ� �α� ����
    public void CalcMaxPopulation(AntType type, int value)
    {
        switch (type)
        {
            case AntType.Worker:
                if (maxWorker + value < 0)
                {
                    Debug.LogError("Can't Reduce MaxCap Below 0");
                    return;
                }
                maxWorker += value;
                break;
            case AntType.Scout:
                if (maxScout + value < 0)
                {
                    Debug.LogError("Can't Reduce MaxCap Below 0");
                    return;
                }
                maxScout += value;
                break;
            case AntType.Soldier:
                if (maxSoldier + value < 0)
                {
                    Debug.LogError("Can't Reduce MaxCap Below 0");
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
                    Debug.LogError("Reached MaxCap");
                    return;
                }
                currentWorker += value;
                break;
            case AntType.Scout:
                if (currentScout + value > maxScout)
                {
                    Debug.LogError("Reached MaxCap");
                    return;
                }
                currentScout += value;
                break;
            case AntType.Soldier:
                if (currentSoldier + value > maxSoldier)
                {
                    Debug.LogError("Reached MaxCap");
                    return;
                }
                currentSoldier += value;
                break;
        }
    }
    #endregion
}
