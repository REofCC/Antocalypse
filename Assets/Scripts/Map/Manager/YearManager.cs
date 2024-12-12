using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearManager : MonoBehaviour
{
    int currentYear;
    int requireFood;
    int requireLeaf;
    int requireWood;

    float currentTime;
    float timeLimit;

    int[] requireFoodArray = { 50, 100, 200, 300, 500 };
    int[] requireLeafArray = {50, 100, 200, 300, 500};
    int[] requireWoodArray = {0, 30, 50, 100, 300};
    public void Init()
    {
        currentYear = 1;
        requireFood = 100;
        requireLeaf = 100;
        requireWood = 0;

        currentTime = 0;
        timeLimit = 600;

        StartCoroutine(Timer());
    }
    public int GetCurrentYear()
    {
        return currentYear;
    }
    public float GetFillAmount()    //겨울 진행 바 채움 정도
    {
        return timeLimit / currentTime;
    }
    public int GetRequireResource(Resourcetype resouceType)
    {
        switch (resouceType)
        {
            case Resourcetype.Leaf:
                return requireLeaf;
            case Resourcetype.Wood:
                return requireWood;
            case Resourcetype.Liquid:
                return requireFood;
        }
        return -1;
    }
    void RequirementEvnet()
    {
        return;
    }
    void NextRequirement()
    {
        currentTime = 0;
        currentYear++;
        requireFood = requireFoodArray[currentYear - 1];
        requireLeaf = requireLeafArray[currentYear - 1];
        requireLeaf = requireWoodArray[currentYear - 1];
    }
    IEnumerator Timer()
    {
        while (timeLimit > currentTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        RequirementEvnet();
    }
}
