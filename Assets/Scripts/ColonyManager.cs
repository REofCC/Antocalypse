using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyManager : MonoBehaviour
{
    [SerializeField]
    protected int maxLeaf;
    [SerializeField]
    protected int currentLeaf;


    public void UseResoruce(int value)  // 추후 자원 종류에 따라 switch문
    {
        if (currentLeaf < value)
        {
            Debug.Log("require more resources");
        }
        else
        {
            currentLeaf -= value;
            Debug.Log(value + " used");
        }
    }
    public void GetResoruce(int value)
    {
        if (maxLeaf < value + currentLeaf)
        {
            currentLeaf = maxLeaf;
            Debug.Log("Cargo Full");
        }
        else
        {
            currentLeaf += value;
            Debug.Log(value + " get");
        }
    }
}
