using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    AntType antType;
    float spawnTime;
    float t;
    GameObject ant;
    public void SetValue(AntType _antType, float _spawnTime)
    {
        antType = _antType;
        spawnTime = _spawnTime;
        t = 0;
        switch (antType)
        {
            case AntType.Worker:
                ant = Resources.Load<GameObject>("Prefabs/Ant/Worker");
                break;
            case AntType.Scout:
                ant = Resources.Load<GameObject>("Prefabs/Ant/Scout");
                break;
            case AntType.Soldier:
                ant = Resources.Load<GameObject>("Prefabs/Ant/Soldier");
                break;
        }
        StartCoroutine(SpawnTimer());
    }
    IEnumerator SpawnTimer()
    {
        while (t <= spawnTime)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Instantiate(ant, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
