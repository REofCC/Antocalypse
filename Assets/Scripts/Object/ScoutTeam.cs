using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutTeam : MonoBehaviour
{
    int scoutCount;
    float buffValue;
    float moveSpeed;

    public bool CanMakeTeam()   //scout �ּ�ġ �̻� ����������
    {
        int currentIdleScout = 1;   //�ӽ� �ڵ�
        // idle ������ scout �� üũ
        int requirement = 5 +(int)Managers.EvoManager.GetCurrentBuff(BuffType.MinScout);    //�⺻�� - ����
        if (currentIdleScout >= requirement)
            return true;
        else
            return false;
    }
    public void Init(int _scoutCount) // �ο�����ŭ �ʱ�ȭ
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
    public void OnScoutFinish(int remainResource)   //���� ��
    {
       //�̺�Ʈ ���
    }
    void Return()   //���� doorNode�� ��ȯ
    {

    }
    void OnReturn()
    {
        // �Ա� ��ġ�� scoutCount ��ŭ Scout �ν��Ͻ�ȭ
        Destroy(gameObject);
    }
}
