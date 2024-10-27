using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyManager
{
    [SerializeField]
    protected int maxLeaf = 100;
    [SerializeField]
    protected int currentLeaf = 0;
    public void UseResoruce(int value)  // ���� �ڿ� ������ ���� switch��
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
