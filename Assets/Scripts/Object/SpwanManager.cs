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
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                // spawnTime = _spawnTime ���� �ð� �߰�
                break;
            case "Scout":
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                // spawnTime = _spawnTime ���� �ð� �߰�
                break;
            case "Soldier":
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                // spawnTime = _spawnTime ���� �ð� �߰�
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
