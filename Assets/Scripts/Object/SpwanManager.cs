using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    float spawnTime;
    GameObject ant;
    public void SpawnAnt(string antType)
    {
        switch (antType)
        {
            case "Worker":
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 5.0f; //스폰 시간 추가
                ant = Resources.Load<GameObject>("Prefabs/Ant/Worker");
                break;
            case "Scout":
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 8.0f; //스폰 시간 추가
                break;
            case "Soldier":
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 10.0f; //스폰 시간 추가
                break;
        }
        StartCoroutine(StartSpawn(spawnTime, ant));
    }

    IEnumerator StartSpawn(float timer, GameObject ant)
    {
        float t = 0; 
        while (t <= timer)
        {
            t += Time.deltaTime;
            yield return null;
        }
       
        Instantiate(ant, Vector3.zero, Quaternion.identity);    //스폰 위치 변경 추가
    }
}
