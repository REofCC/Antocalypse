using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyManager : MonoBehaviour
{
    private static ColonyManager instance = null;

    [SerializeField]
    protected int maxLeaf;
    [SerializeField]
    protected int currentLeaf;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static ColonyManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
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
