
using UnityEngine;

public class EvoManager
{
    AntType buffTargetType;

    int evoCount;

    bool canResearchMine;  // 심층 채굴기 연구 가능
    bool canResearchOutpost;   //전초기지 연구 가능

    float[] currentBuff;  //적용 중 버프
    bool[] currentBoolBuff;    //해금?
    EvoData worker;
    EvoData scout;
    EvoData soldier;
    
    public void Init()
    {
        worker = Resources.Load<EvoData>("Evo/Worker");
        scout = Resources.Load<EvoData>("Evo/Scout");
        soldier = Resources.Load<EvoData>("Evo/Soldier");
        currentBuff = new float[(int)BuffType.EndOfFloatBuff - 1];
        currentBoolBuff = new bool[(int)BuffType.EndOfBoolBuff - (int)BuffType.EndOfFloatBuff - 1];
    }
    //public void AddCurrentBuff(BuffType type, float value)
    //{
    //    if ((int)type < (int)BuffType.EndOfFloatBuff)
    //    {
    //        currentBuff[(int)type] += value;
    //    }
    //    else
    //    {
    //        if (value == 1)
    //            currentBoolBuff[(int)type - (int)BuffType.EndOfFloatBuff - 1] = true;
    //        else if(value == -1)
    //            currentBoolBuff[(int)type - (int)BuffType.EndOfFloatBuff -1] = false;
    //    }
    //}
    public void AddCurrentBuff(BuffPart part, int branch)  //진화 선택 완료시 호출
    {
        BuffType type = GetBuffType(buffTargetType, part, branch);
        float value = GetBuffValue(buffTargetType, part, branch);

        if ((int)type < (int)BuffType.EndOfFloatBuff)
        {
            currentBuff[(int)type] += value;
        }
        else
        {
            if (value == 1)
                currentBoolBuff[(int)type - (int)BuffType.EndOfFloatBuff - 1] = true;
            else if (value == -1)
                currentBoolBuff[(int)type - (int)BuffType.EndOfFloatBuff - 1] = false;
        }
    }
    public void AddEvoCount()   //진화 선택 완료시 호출
    {
        evoCount++;
    }
    public void SetBuffTargetType(AntType type) //특화 병종 선택 시 호출
    {
        buffTargetType = type;
        switch (type) {
            case AntType.Worker:
                currentBoolBuff[(int)BuffType.CanResearchMine - (int)BuffType.EndOfFloatBuff] = true;
                break;
            case AntType.Scout:
                currentBoolBuff[(int)BuffType.CanResearchOutpost - (int)BuffType.EndOfFloatBuff] = true;
                break;
            case AntType.Soldier:
                currentBoolBuff[(int)BuffType.CanGetCombatReward - (int)BuffType.EndOfFloatBuff] = true;
                break;
        }
    }
    public string GetBuffDescription(AntType antType, BuffPart part, int branch)
    {
        switch (antType)
        {
            case AntType.Worker:
                return worker.GetBuffDescription(part, branch);
                break;
            case AntType.Scout:
                return scout.GetBuffDescription(part, branch);
                break;
            case AntType.Soldier:
                return soldier.GetBuffDescription(part, branch);
                break;
        }
        return null;
    }
    public BuffType GetBuffType(AntType antType, BuffPart part, int branch)
    {
        switch (antType)
        {
            case AntType.Worker:
                return worker.GetBuffType(part, branch);
                break;
            case AntType.Scout:
                return scout.GetBuffType(part, branch);
                break;
            case AntType.Soldier:
                return soldier.GetBuffType(part, branch);
                break;
        }
        return BuffType.EndOfFloatBuff;
    }
    public float GetBuffValue(AntType antType, BuffPart part, int branch)   //-1은 false, 1은 true와 동일
    {
        switch (antType)
        {
            case AntType.Worker:
                return worker.GetBuffValue(part, branch);
                break;
            case AntType.Scout:
                return scout.GetBuffValue(part, branch);
                break;
            case AntType.Soldier:
                return soldier.GetBuffValue(part, branch);
                break;
        }
        return 0;
    }
    public AntType GetBuffAntType()
    {
        return buffTargetType;
    }
    public float GetCurrentBuff(BuffType buffType)
    {
        return currentBuff[(int)buffType];
    }
    public bool GetCurrentBoolBuff(BuffType buffType)
    {
        return currentBoolBuff[(int)buffType - (int)BuffType.EndOfFloatBuff - 1];
    }
    public int GetCurrentEvoCount()
    {
        return evoCount;
    }
}
