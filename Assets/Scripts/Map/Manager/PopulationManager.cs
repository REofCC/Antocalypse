public class PopulationManager
{
    #region Attribute
    int spawnResourceDecs;
    float spawnDescPerc;
    int maxPopulation;
    #endregion
    #region Getter & Setter
    public float GetSpawnDesc()
    {
        return spawnDescPerc;
    }
    public int GetMaxPopulation()
    {
        return maxPopulation;
    }

    #endregion
    #region Function
    public void CalcSpawnResourceDesc(int value)
    {
        spawnResourceDecs += value;
        spawnDescPerc = (spawnResourceDecs)/(spawnResourceDecs+100);
    }
    public void CalcMaxPopulation(int value)
    {
        maxPopulation += value;
    }
    #endregion
}
