using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    #region Event
    public event Action<ResourceType> OnResourceChange; 

    #endregion
    #region Attribute
    int liquidFood = 0;
    int solidFood = 0;
    int leaf = 0;
    int wood = 0;
    int genetic = 0;

    int maxLeaf = 0;
    int maxWood = 0;
    int maxSolidFood = 0;
    int maxLiquidFood = 0;
    int maxGenetic = 0;

    public ResourceGroup resGroup= new();
    #endregion
    #region Getter & Setter
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
        OnResourceChange?.Invoke(ResourceType.LIQUID_FOOD);
    }
    private void SetSolidFood(int value)
    {
        solidFood = value;
        OnResourceChange?.Invoke(ResourceType.SOLID_FOOD);
    }
    private void SetLeaf(int value)
    {
        leaf = value;
        OnResourceChange?.Invoke(ResourceType.LEAF);
    }
    private void SetWood(int value)
    {
        wood = value;
        OnResourceChange?.Invoke(ResourceType.WOOD);
    }
    private void SetGenetic(int value)
    {
        genetic = value;
        OnResourceChange?.Invoke(ResourceType.GENETIC_MATERIAL);
    }
    private void SetMaxSolidFood(int value)
    {
        maxSolidFood = value;
        OnResourceChange?.Invoke(ResourceType.SOLID_FOOD);
    }
    private void SetMaxLiquidFood(int value)
    {
        maxLiquidFood = value;
        OnResourceChange?.Invoke(ResourceType.LIQUID_FOOD);
    }
    public int GetMaxLeaf()
    {
        return maxLeaf;
    }
    public int GetMaxWood()
    {
        return maxWood;
    }
    public int GetMaxGenetic()
    {
        return maxGenetic;
    }
    public int GetMaxSolidFood()
    {
        return maxSolidFood;
    }
    public int GetMaxLiquidFood()
    {
        return maxLiquidFood;
    }
    public void SetMaxLeaf(int value)
    {
        maxLeaf = value;
    }
    public void SetMaxWood(int value)
    {
        maxWood = value;
    }
    #endregion
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
    #region Function
    public void CalcMaxSolidFood(int value)
    {
        SetMaxSolidFood(GetMaxSolidFood() + value);
    }
    public void CalcMaxLiquidFood(int value)
    {
        SetMaxLiquidFood(GetMaxLiquidFood() + value);
    }
    public void CalcMaxLeaf(int value)
    {
        SetMaxLeaf(GetMaxLeaf() + value);
    }
    public void CalcMaxWood(int value)
    {
        SetMaxWood(GetMaxWood() + value);
    }
    public bool ChangeFood(int solid, float time, float ratio)
    {
        if (MinusSolidFood(solid))
        {
            Managers.Manager.StartCoroutine(ChangeFoodCoroutine(solid, time, ratio));
            return true;
        }
        return false;
    }
    #endregion
    #region Coroutine
    IEnumerator ChangeFoodCoroutine(int solid, float time, float ratio)
    {
        yield return new WaitForSeconds(time);
        AddLiquidFood(((int)(solid * ratio)));
        yield break;
    }
    #endregion
}
