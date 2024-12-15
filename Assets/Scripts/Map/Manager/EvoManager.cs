using UnityEngine;

public class EvoManager : MonoBehaviour
{
    AntType buffTargetType;

    int evoCount;

    bool canResearchMine;  // ���� ä���� ���� ����
    bool canResearchOutpost;   //���ʱ��� ���� ����

    float[] currentBuff;  //���� �� ����

    public void AddCurrentBuff(BuffType type, float value)  //��ȭ ���� �Ϸ�� ���� �������� ȣ��
    {
        currentBuff[(int)type] += value;
    }
    public void AddEvoCount()   //��ȭ ���� �Ϸ�� ȣ��
    {
        evoCount++;
    }
    public void SetBuffTargetType(AntType type) //Ưȭ ���� ���� �� ȣ��
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
