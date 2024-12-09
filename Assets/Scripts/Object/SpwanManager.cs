using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    float spawnTime;
    public void SpawnAnt(string antType)
    {
        switch (antType)
        {
            case "Worker":
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                // spawnTime = _spawnTime 스폰 시간 추가
                break;
            case "Scout":
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                // spawnTime = _spawnTime 스폰 시간 추가
                break;
            case "Soldier":
                // if(cost > currentResource)자원 소비 체크
                // break;
                // else
                //  currentResource -=cost
                // spawnTime = _spawnTime 스폰 시간 추가
                break;
        }
        StartCoroutine(StartSpawn(spawnTime));
    }

    IEmulator StartSpawn(float timer)
    {
        float t = 0; 
        while (t <= timer)
        {
            t += time.Deltatime;
        }
        //instantiate
    }
}
