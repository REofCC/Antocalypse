using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/ResourceEvent")]
public class EventResourceData : EventData
{
    #region Attribute
    [SerializeField]
    int leafAmount;
    [SerializeField]
    int woodAmount;
    [SerializeField]
    int liquidFoodAmount;
    [SerializeField]
    int solidFoodAmount;
    #endregion

    #region Getter & Setter
    public int LeafAmount { get { return leafAmount; } }
    public int WoodAmount { get {return woodAmount; } }
    public int LiquidFoodAmount { get { return liquidFoodAmount; } }
    public int SolidFoodAmount { get {  return solidFoodAmount; } }
    #endregion
}
