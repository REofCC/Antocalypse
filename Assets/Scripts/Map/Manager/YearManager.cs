using System;
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

    public event Action OnWinterEvent;

    public event Action OnTimeEvent;

    public void Init()
    {
        currentYear = 0;
        timeLimit = 10;
        SetRequirement();
        OnWinterEvent?.Invoke();
        StartNextYear();        
    }
    public int GetCurrentYear()
    {
        return currentYear;
    }
    public float GetFillAmount()    //겨울 진행 바 채움 정도
    {
        return  currentTime / timeLimit;
    }
    public int GetRequireResource(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.LEAF:
                return requireLeaf;
            case ResourceType.WOOD:
                return requireWood;
            case ResourceType.LIQUID_FOOD:
                return requireFood;
        }
        return -1;
    }
    void RequirementEvent() //정산 이벤트
    {
        OnWinterEvent?.Invoke();
        return;
    }
    void SetRequirement()
    {
        requireFood = requireFoodArray[currentYear];
        requireLeaf = requireLeafArray[currentYear];
        requireWood = requireWoodArray[currentYear];
    }
    public void StartNextYear() //다음 연차 실행
    {
        currentTime = 0;
        currentYear++;        
        SetRequirement();
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {        
        while (timeLimit > currentTime)
        {
            currentTime += 1;            
            OnTimeEvent?.Invoke();
            yield return new WaitForSeconds(1);
        }
        RequirementEvent();        
    }
}
