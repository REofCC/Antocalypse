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

    public void Init(int _soldierCount) // 인원수만큼 초기화
    {
        soldierCount = _soldierCount;
        totalCombatPower = _soldierCount * 50; //기본 CP
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
            if (lossReduce!=0)   //병정개미 소모 감소 있을 시
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
            if (Managers.EvoManager.GetCurrentBuff(BuffType.CanGetCombatReward) == 1)   //병정개미 특화 시
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
    public void OnBattleWin()   //전투 승리 후 노드 교체 및 귀환
    {
    }
    public void SetEnemyCp(float value)  //해당 적 전투 후 CP로 설정
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
        // 입구 위치에 soldierCount 만큼 Soldier 인스턴스화

        Destroy(gameObject);
    }
    ResourceType GetEnemyResourceData()
    {
        return ResourceType.LEAF;   //임시 코드, 변경 필요
    }
    int GetEnemyResourceValue()
    {
        return 1;   //임시 코드, 변경 필요
    }
}
