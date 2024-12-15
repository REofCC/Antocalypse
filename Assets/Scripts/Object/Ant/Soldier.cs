using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Ant
{
    #region Attribute
    float hp;
    float atk;
    #endregion
    #region Function
    #region Public
    #endregion
    #region Private
    protected override void SetBT()
    {
        root = new BTSelector();
        BTSelector orderSelector = new BTSelector();

        BTSequence eatSequence = new BTSequence();
        BTSequence idleSequence = new BTSequence();

        BTAction eatAction = new BTAction(Eat);
        BTAction moveAction = new BTAction(Move);
        BTAction idleAction = new BTAction(Idle);

        BTCondition kcalLow = new BTCondition(IsKcalLow);

        root.AddChild(eatSequence);
        root.AddChild(orderSelector);
        root.AddChild(idleSequence);

        eatSequence.AddChild(kcalLow);
        //eatSequence.AddChild(moveAction);
        eatSequence.AddChild(eatAction);

        idleSequence.AddChild(idleAction);
        idleSequence.AddChild(moveAction);
    }
    #endregion
    #region Unity
    private void Start()
    {
        SetBT();
        root.Evaluate();
    }
    private void FixedUpdate()
    {
        root.Evaluate();
    }
    #endregion
    #endregion
    #region BT
    #endregion
}
