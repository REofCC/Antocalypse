using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoManager : MonoBehaviour
{
    AntType buffTargetType;
    int evoCount;
    // 공통 버프
    float resoruceCapBuff;  //운반 가능한 자원 상한
    float movementBuff; //이동 속도
    float maxCapBuff;   //부화지가 제공하는 상한
    float spawnCostBuff;    //부화 비용
    // 일개미 전용 버프
    float gatherSpeedBuff;  //채집 속도
    float buildSpeedBuff;   //건설,파괴,땅 파기 속도
    float foodConsumeBuff;  //식량 소모
    bool canBuildMine;  //심층 채굴기 건설 가능
    // 정찰대 전용 버프
    int sightBuff;  //지상 시야
    int minScoutBuff;   //최소 탐사 인원
    bool canBuildOutpost;   //전초기지 건설 가능
    //병정개미 전용 버프
    float battleScoreBuff;  //전투력
    float battleRewardBuff; //전투 승리 보상
    float lossBuff;  //개체 손실 감소
}
