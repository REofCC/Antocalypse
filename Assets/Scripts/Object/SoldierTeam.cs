using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTeam : MonoBehaviour
{
    int soldierCount;
    float totalCombatPower;
    float buffValue;
    ResourceType resourceType;
    int resourceValue;
    float enemyCP;

    public void Init(int _soldierCount) // �ο�����ŭ �ʱ�ȭ
    {
        soldierCount = _soldierCount;
        totalCombatPower = _soldierCount * 50; //�⺻ CP
    }
    public void Combat (float _enemyCP)
    {
        enemyCP = _enemyCP;
        ApplyBuff();
        while (true)
        {
            enemyCP -= totalCombatPower / 2;
            if (enemyCP <= 0)
            {
                CombatResult(true);
                break;
            }
            totalCombatPower -= enemyCP / 2;
            if (totalCombatPower <= 0)
            {
                CombatResult(false);
                break;
            }
        }
    }
    void ApplyBuff()
    {
        buffValue = Managers.EvoManager.GetCurrentBuff(BuffType.CombatPower);

        if (buffValue != 0)
        {
            totalCombatPower += totalCombatPower * buffValue;
        }
    }
    void CalcRemain(bool win)
    {
        if (win)
        {
            int loss;
            if (buffValue != 0)
                loss = (int)(totalCombatPower / (buffValue * 50f + 50f));
            else
                loss = (int)(totalCombatPower / 50);
            float lossReduce = Managers.EvoManager.GetCurrentBuff(BuffType.CombatLoss);
            if (lossReduce!=0)   //�������� �Ҹ� ���� ���� ��
            {
                loss -= (int)(loss * lossReduce);
            }
            soldierCount -= loss;
        }
        else
        {
            soldierCount = 0;
        }
    }
    public void CombatResult(bool win)
    {
        CalcRemain(win);
        Managers.Population.CalcCurrentPopulation(AntType.Soldier, soldierCount);
        if (win)
        {
            if (Managers.EvoManager.GetCurrentBuff(BuffType.CanGetCombatReward) == 1)   //�������� Ưȭ ��
            {
                resourceType = GetEnemyResourceData();
                resourceValue = GetEnemyResourceValue();
            }
            OnBattleWin();
        }
        else
        {
            SetEnemyCp(enemyCP); 
            Destroy(gameObject);
        }
    }
    public void OnBattleWin()   //���� �¸� �� ��� ��ü �� ��ȯ
    {
    }
    public void SetEnemyCp(float value)  //�ش� �� ���� �� CP�� ����
    {
    }
    public void OnReturn()
    {
        switch(resourceType)
        {
            case ResourceType.LEAF:
                Managers.Resource.AddLeaf(resourceValue); 
                break;
            case ResourceType.WOOD:
                Managers.Resource.AddLeaf(resourceValue);
                break;
            case ResourceType.LIQUID_FOOD:
                Managers.Resource.AddLeaf(resourceValue);
                break;
            case ResourceType.SOLID_FOOD:
                Managers.Resource.AddLeaf(resourceValue);
                break;
        }
        // �Ա� ��ġ�� soldierCount ��ŭ Soldier �ν��Ͻ�ȭ

        Destroy(gameObject);
    }
    ResourceType GetEnemyResourceData()
    {
        return ResourceType.LEAF;   //�ӽ� �ڵ�, ���� �ʿ�
    }
    int GetEnemyResourceValue()
    {
        return 1;   //�ӽ� �ڵ�, ���� �ʿ�
    }
}
