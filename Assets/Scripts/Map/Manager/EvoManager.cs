using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoManager : MonoBehaviour
{
    // ToDo : ���� ������ ����, ���� ���� ������ ���� �⺻ Ŭ������ ��ӹ޾� ���, � �������� Enum�� �̿��� (Enumtype.������) ȣ�� �� ���� ��ġ ��ȯ �Լ�
    AntType buffTargetType;
    int evoCount;
    // ���� ����
    float resoruceCapBuff;  //��� ������ �ڿ� ����
    float movementBuff; //�̵� �ӵ�
    float maxCapBuff;   //��ȭ���� �����ϴ� ����
    float spawnCostBuff;    //��ȭ ���
    // �ϰ��� ���� ����
    float gatherSpeedBuff;  //ä�� �ӵ�
    float buildSpeedBuff;   //�Ǽ�,�ı�,�� �ı� �ӵ�
    float foodConsumeBuff;  //�ķ� �Ҹ�
    bool canBuildMine;  //���� ä���� �Ǽ� ����
    // ������ ���� ����
    int sightBuff;  //���� �þ�
    int minScoutBuff;   //�ּ� Ž�� �ο�
    bool canBuildOutpost;   //���ʱ��� �Ǽ� ����
    //�������� ���� ����
    float battleScoreBuff;  //������
    float battleRewardBuff; //���� �¸� ����
    float lossBuff;  //��ü �ս� ����
}
