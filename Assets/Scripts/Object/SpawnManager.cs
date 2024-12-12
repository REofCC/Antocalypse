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
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 5.0f; //���� �ð� �߰�
                break;
            case AntType.Scout:
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 8.0f; //���� �ð� �߰�
                break;
            case AntType.Soldier:
                // if(cost > currentResource)�ڿ� �Һ� üũ
                // break;
                // else
                //  currentResource -=cost
                spawnTime = 10.0f; //���� �ð� �߰�
                break;
        }
        egg = Resources.Load<GameObject>("Prefabs/Ant/Egg");
        egg = Instantiate(egg, MapManager.Map.MapMaker.GetStartPos().GetWorldPos(), Quaternion.identity);    //Todo ���� ��ġ ����
        egg.GetComponent<Egg>().SetValue(antType, spawnTime);
    }
}
