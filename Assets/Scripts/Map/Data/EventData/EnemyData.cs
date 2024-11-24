using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/Enemy")]
public class EnemyData : EventData
{
    #region Attribute
    [SerializeField]
    float combatPower;
    #endregion

    #region Getter & Setter
    public float CombatPower { get { return combatPower; } }
    #endregion
}
