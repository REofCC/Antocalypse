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

    public void Init(int _scoutCount) // 인원수만큼 초기화
    {
        scoutCount = _scoutCount;
        maxHoldValue = _scoutCount * 20; //기본 자원 운반량
        moveSpeed = 1;
    }
    public void Gather()
    {
        ApplyBuff();
        int resourceNodeValue = GetNodeResourceValue();
        if (resourceNodeValue > maxHoldValue)   //채집하고 노드에 자원 남을 때
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
    public void OnGatherFinish(int remainResource)   //채취 후
    {
        if (remainResource == 0)    // 자원노드 고갈 시
        {
            //일반 노드로 교체
        }
        else
        {
            SetRemainResource(remainResource);
        }
        Return();
    }
    void Return()   //지상 doorNode로 귀환
    {

    }
    void SetRemainResource(float value)  //채집 이후 자원 노드 값 변경
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
        // 입구 위치에 scoutCount 만큼 Scout 인스턴스화

        Destroy(gameObject);
    }
    ResourceType GetNodeResourceData()
    {
        return ResourceType.LEAF;   //임시 코드, 변경 필요
    }
    int GetNodeResourceValue()
    {
        return 1;   //임시 코드, 변경 필요
    }
}
