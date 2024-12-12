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
        timeLimit = 600;

        SetRequirement();        
        StartNextYear();
    }
    public int GetCurrentYear()
    {
        return currentYear;
    }
    public float GetFillAmount()    //�ܿ� ���� �� ä�� ����
    {
        return  currentTime / timeLimit;
    }
    public int GetRequireResource(Resourcetype resourceType)
    {
        switch (resourceType)
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
    void RequirementEvent() //���� �̺�Ʈ
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
    public void StartNextYear() //���� ���� ����
    {
        currentTime = 0;
        currentYear++;
        OnWinterEvent?.Invoke();
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
