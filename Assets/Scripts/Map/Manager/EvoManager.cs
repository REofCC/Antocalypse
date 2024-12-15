using UnityEngine;

public class EvoManager : MonoBehaviour
{
    AntType buffTargetType;

    int evoCount;

    bool canResearchMine;  // 심층 채굴기 연구 가능
    bool canResearchOutpost;   //전초기지 연구 가능

    float[] currentBuff;  //적용 중 버프

    public void AddCurrentBuff(BuffType type, float value)  //진화 선택 완료시 적용 버프마다 호출
    {
        currentBuff[(int)type] += value;
    }
    public void AddEvoCount()   //진화 선택 완료시 호출
    {
        evoCount++;
    }
    public void SetBuffTargetType(AntType type) //특화 병종 선택 시 호출
    {
        buffTargetType = type;
        currentBuff = new float[(int)BuffType.None - 1];
    }
    public AntType GetBuffAntType()
    {
        return buffTargetType;
    }
    public float GetCurrentBuff(BuffType buffType)
    {
        return currentBuff[(int)buffType];
    }
    public bool GetCanBuildMine()
    {
        return canResearchMine;
    }
    public bool GetCanBuildOutpost()
    {
        return canResearchOutpost;
    }
}
