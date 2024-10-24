using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameState : MonoBehaviour
{
    #region Global Instance
    static GameState instance;
    public static GameState Instance{ get { return instance; } }
    #endregion
    #region Attribute
    int liquidFood = 0;
    int solidFood = 0;
    int leaf = 0;
    int wood = 0;
    int genetic = 0;

    int spawnResourceDecs;
    float spawnDescPerc;
    int maxPopulation;
    int maxSolidFood;
    int maxLiquidFood;

    #endregion
    #region Getter & Setter
    #region Resource
    public int GetLiquidFood()
    {
        return liquidFood;
    }
    public int GetSolidFood()
    {
        return solidFood;
    }
    public int GetLeaf()
    {
        return leaf;
    }
    public int GetWood()
    {
        return wood;
    }
    public int GetGenetic()
    {
        return genetic;
    }
    private void SetLiquidFood(int value)
    {
        liquidFood = value;
    }
    private void SetSolidFood(int value)
    {
        solidFood = value;
    }
    private void SetLeaf(int value)
    {
        leaf = value;
    }
    private void SetWood(int value)
    {
        wood = value;
    }
    private void SetGenetic(int value)
    {
        genetic = value;
    }
    #endregion
    #region Building Effect
    public float GetSpawnDesc()
    {
        return spawnDescPerc;
    }
    #endregion
    public int GetMaxPopulation()
    {
        return maxPopulation;
    }
    public int GetMaxSolidFood()
    {
        return maxSolidFood;
    }
    public int GetMaxLiquidFood()
    {
        return maxLiquidFood;
    }
    #endregion
    #region Function
    #region Check
    public bool CheckLiquidFood(int value)
    {
        return GetLiquidFood() >= value;
    }
    public bool CheckSolidFood(int value)
    {
        return GetSolidFood() >= value;
    }
    public bool CheckLeaf(int value)
    {
        return GetLeaf() >= value;
    }
    public bool CheckWood(int value)
    {
        return GetWood() >= value;
    }
    public bool CheckGenetic(int value)
    {
        return GetGenetic() >= value;
    }
    #endregion
    #region Calculation
    public void AddSolidFood(int value)
    {
        SetSolidFood(value + GetSolidFood());
    }
    public void AddLiquidFood(int value)
    {
        SetLiquidFood(value + GetLiquidFood());
    }
    public void AddLeaf(int value)
    {
        SetLeaf(value + GetLeaf());
    }
    public void AddWood(int value)
    {
        SetWood(value + GetWood());
    }
    public void AddGenetic(int value)
    {
        SetGenetic(value + GetGenetic());
    }
    public bool MinusSolidFood(int value)
    {
        if (!CheckSolidFood(value))
            return false;
        SetSolidFood(GetLiquidFood() - value);
        return true;
    }
    public bool MinusLiquidFood(int value)
    {
        if (!CheckLiquidFood(value))
            return false;
        SetLiquidFood(GetLiquidFood() - value);
        return true;
    }
    public bool MinusLeaf(int value)
    {
        if (!CheckLeaf(value))
            return false;
        SetLeaf(GetLeaf() - value);
        return true;
    }
    public bool MinusWood(int value)
    {
        if (!CheckWood(value))
            return false;
        SetWood(GetWood() - value);
        return true;
    }
    public bool MinusGenetic(int value)
    {
        if (!CheckGenetic(value))
            return false;
        SetGenetic(GetGenetic() - value);
        return true;
    }
    #endregion
    #region Building Effect
    public void CalcSpawnResourceDesc(int value)
    {
        spawnResourceDecs += value;
        spawnDescPerc = (spawnResourceDecs)/(spawnResourceDecs+100);
    }
    public void CalcMaxPopulation(int value)
    {
        maxPopulation += value;
    }
    public void CalcMaxSolidFood(int value)
    {
        maxSolidFood += value;
    }
    public void CalcMaxLiquidFood(int value)
    {
        maxLiquidFood += value;
    }

    #endregion
    #endregion
    #region Unity Function
    private void Awake()
    {
        instance = GetComponent<GameState>();
    }
    #endregion
}
