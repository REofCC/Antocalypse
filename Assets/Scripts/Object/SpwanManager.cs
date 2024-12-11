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
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 5.0f; //���� �ð� �߰�
                ant = Resources.Load<GameObject>("Prefabs/Ant/Worker");
                break;
            case "Scout":
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 8.0f; //���� �ð� �߰�
                break;
            case "Soldier":
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 10.0f; //���� �ð� �߰�
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
       
        Instantiate(ant, Vector3.zero, Quaternion.identity);    //���� ��ġ ���� �߰�
    }
}
