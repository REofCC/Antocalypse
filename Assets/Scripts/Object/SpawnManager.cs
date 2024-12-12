using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    AntType antType;
    float spawnTime;
    GameObject egg;
    public void SpawnEgg(AntType _antType)
    {
        antType = _antType;
        switch (_antType)
        {
            case AntType.Worker:
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 5.0f; //스폰 시간 추가
                break;
            case AntType.Scout:
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 8.0f; //스폰 시간 추가
                break;
            case AntType.Soldier:
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 10.0f; //스폰 시간 추가
                break;
        }
        egg = Resources.Load<GameObject>("Prefabs/Ant/Egg");
        egg = Instantiate(egg, MapManager.Map.MapMaker.GetStartPos().GetWorldPos(), Quaternion.identity);    //Todo 스폰 위치 조정
        egg.GetComponent<Egg>().SetValue(antType, spawnTime);
    }
}
