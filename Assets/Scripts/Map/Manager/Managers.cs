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

    static SpawnManager spawnManager = new();
    public static SpawnManager SpawnManager { get { return spawnManager; } }

    static YearManager yearManager;
    public static YearManager YearManager { get { return yearManager; } }

    static TaskManager taskManager = new();
    public static TaskManager Task { get { return taskManager; } }

    static EvoManager evoManager = new();
    public static EvoManager EvoManager { get { return evoManager; } }
    private void Awake()
    {
        // 권희준 - 코루틴 사용을 위해 GameObject에 추가
        yearManager = gameObject.AddComponent<YearManager>();
        yearManager.Init();
        taskManager = gameObject.AddComponent<TaskManager>();
        taskManager.Init();
    }


    private void Start()
    {
        manager = GetComponent<Managers>();
        DontDestroyOnLoad(manager);

    }
}
