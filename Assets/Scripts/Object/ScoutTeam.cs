using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutTeam : MonoBehaviour
{
    int scoutCount;
    float buffValue;
    float moveSpeed;

    public bool CanMakeTeam()   //scout 최소치 이상 보유중인지
    {
        int currentIdleScout = 1;   //임시 코드
        // idle 상태의 scout 수 체크
        int requirement = 5 +(int)Managers.EvoManager.GetCurrentBuff(BuffType.MinScout);    //기본값 - 버프
        if (currentIdleScout >= requirement)
            return true;
        else
            return false;
    }
    public void Init(int _scoutCount) // 인원수만큼 초기화
    {
        scoutCount = _scoutCount;
        moveSpeed = 1;
    }
    public void Scout()
    {
        ApplyBuff();
    }
    void ApplyBuff()
    {
        buffValue = Managers.EvoManager.GetCurrentBuff(BuffType.MoveSpeed);

        if (buffValue != 0)
        {
            moveSpeed += moveSpeed * buffValue;
        }
    }
    public void OnScoutFinish(int remainResource)   //정찰 후
    {
       //이벤트 대기
    }
    void Return()   //지상 doorNode로 귀환
    {

    }
    void OnReturn()
    {
        // 입구 위치에 scoutCount 만큼 Scout 인스턴스화
        Destroy(gameObject);
    }
}
