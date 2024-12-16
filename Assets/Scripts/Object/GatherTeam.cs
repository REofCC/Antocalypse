using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTeam : MonoBehaviour
{
    int scoutCount;
    float buffValue;
    ResourceType resourceType;
    int resourceValue;
    int maxHoldValue;
    float moveSpeed;

    public void Init(int _scoutCount) // �ο�����ŭ �ʱ�ȭ
    {
        scoutCount = _scoutCount;
        maxHoldValue = _scoutCount * 20; //�⺻ �ڿ� ��ݷ�
        moveSpeed = 1;
    }
    public void Gather()
    {
        ApplyBuff();
        int resourceNodeValue = GetNodeResourceValue();
        if (resourceNodeValue > maxHoldValue)   //ä���ϰ� ��忡 �ڿ� ���� ��
        {
            OnGatherFinish(resourceNodeValue - maxHoldValue);
        }
        else
        {
            resourceValue = resourceNodeValue;
            OnGatherFinish(0);
        }
    }
    void ApplyBuff()
    {
        buffValue = Managers.EvoManager.GetCurrentBuff(BuffType.HoldValue);

        if (buffValue != 0)
        {
            maxHoldValue += (int)(maxHoldValue * buffValue);
        }
    }
    public void OnGatherFinish(int remainResource)   //ä�� ��
    {
        if (remainResource == 0)    // �ڿ���� �� ��
        {
            //�Ϲ� ���� ��ü
        }
        else
        {
            SetRemainResource(remainResource);
        }
        Return();
    }
    void Return()   //���� doorNode�� ��ȯ
    {

    }
    void SetRemainResource(float value)  //ä�� ���� �ڿ� ��� �� ����
    {
    }
    void OnReturn()
    {
        switch (resourceType)
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
        // �Ա� ��ġ�� scoutCount ��ŭ Scout �ν��Ͻ�ȭ

        Destroy(gameObject);
    }
    ResourceType GetNodeResourceData()
    {
        return ResourceType.LEAF;   //�ӽ� �ڵ�, ���� �ʿ�
    }
    int GetNodeResourceValue()
    {
        return 1;   //�ӽ� �ڵ�, ���� �ʿ�
    }
}
