using System.Collections;
using UnityEngine;

public class ResourceManager
{
    
    #region Attribute
    int liquidFood = 0;
    int solidFood = 0;
    int leaf = 0;
    int wood = 0;
    int genetic = 0;

    int maxSolidFood = 0;
    int maxLiquidFood = 0;
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
    private void SetMaxSolidFood(int value)
    {
        maxSolidFood = value;
    }
    private void SetMaxLiquidFood(int value)
    {
        maxLiquidFood = value;
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
    public bool ChangeFood(int solid, int liquid, float time, float ratio)
    {
        if (MinusSolidFood(solid))
        {
            Managers.Manager.StartCoroutine(ChangeFoodCoroutine(liquid, time, ratio));
            return true;
        }
        return false;
    }
    #endregion
    #region Coroutine
    IEnumerator ChangeFoodCoroutine(int liquid, float time, float ratio)
    {
        yield return new WaitForSeconds(time);
        AddLiquidFood(((int)(liquid*ratio)));
        yield break;
    }
    #endregion
}
