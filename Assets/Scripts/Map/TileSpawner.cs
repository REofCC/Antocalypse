using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private string sorucepath = "Resources/Tile";
    private GameObject undergroundMap;
    public void Start()
    {
        undergroundMap = GameObject.Find("UnderGroundMap");
    }
}
