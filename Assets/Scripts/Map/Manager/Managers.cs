using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers manager;
    public static Managers Manager { get { return manager; } }

    static ResourceManager res = new();
    public static ResourceManager Resource { get { return res; } }

    static PopulationManager population = new();
    public static PopulationManager Population { get { return population; } }
    // ������ - spwanManager �߰�
    static SpawnManager spawnManager = new();
    public static SpawnManager SpawnManager { get { return spawnManager; } }
    // ������ - yearManager �߰�
    static YearManager yearManager;
    public static YearManager YearManager { get { return yearManager; } }

    private void Awake()
    {
        yearManager = gameObject.AddComponent<YearManager>();        
    }
#region TaskManager �߰� - ������
    static TaskManager _task = new();
    public static TaskManager Task { get { return _task; } }
    #endregion

    private void Start()
    {
        manager = GetComponent<Managers>();
        DontDestroyOnLoad(manager);

        //���� �ʱ⼳�� �� ����
        yearManager.Init();
    }
}
