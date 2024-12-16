using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/Enemy")]
public class EnemyData : EventData
{
    #region Attribute
    [SerializeField]
    float combatPower;
    [SerializeField]
    ResourceType resourceType;
    [SerializeField]
    float resoruceValue;
    #endregion

    #region Getter & Setter
    public float CombatPower { get { return combatPower; } }
    public ResourceType ResourceType { get { return resourceType; } }
    public float ResoruceValue { get { return resoruceValue; } }
    #endregion
}
