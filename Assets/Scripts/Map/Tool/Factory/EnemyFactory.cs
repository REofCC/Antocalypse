using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    #region Attribute
    [SerializeField]
    List<EnemyData> enemyDict = new List<EnemyData>();
    [SerializeField]
    GameObject enemies;
    #endregion

    #region Function
    private EnemyData SelectEnemy()
    {
        int idx = Random.Range(0, enemyDict.Count);
        return enemyDict[idx];
    }

    private GameObject GenerateEnemy(EnemyData data)
    {
        GameObject enemy = Resources.Load<GameObject>($"Prefabs/Enemy/{data.EventName}");
        if (enemy == null)
        {
            Debug.Log($"Error : Prefabs/Enemy/{data.EventName} is not exist");
            return null;
        }
        return Instantiate(enemy, enemies.transform);
    }

    public void GenerateEvent(TravelNode node)
    {
        GameObject obj = GenerateEnemy(SelectEnemy());
        obj.transform.position = node.GetWorldPos();
        obj.GetComponent<EnemyEvent>().SetNode(node);
        node.SetEvent(obj.GetComponent<EnemyEvent>());
    }
    #endregion
}
