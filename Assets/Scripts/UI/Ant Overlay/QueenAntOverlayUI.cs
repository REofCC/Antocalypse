using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TempAntType
{
    Worker,
    Scout,
    Soldier,
    Queen,
}

public class QueenAntOverlayUI : AntOverlayUI
{
    [SerializeField] 
    private void Awake()
    {
    }
    public override void OnClickAnt()
    {
        
    }

    public override void CheckAntType()
    {
        //Check if the ant is a queen ant

    }

    public override void ShowAntOverlay()
    {
        throw new System.NotImplementedException();
    }
}
