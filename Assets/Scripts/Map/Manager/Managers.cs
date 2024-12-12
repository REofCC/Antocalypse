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
    // 권희준 - spwanManager 추가
    static SpawnManager spawnManager = new();
    public static SpawnManager SpawnManager { get { return spawnManager; } }
    // 권희준 - yearManager 추가
    static YearManager yearManager = new();
    public static YearManager YearManager { get { return yearManager; } }

    private void Start()
    {
        manager = GetComponent<Managers>();
        DontDestroyOnLoad(manager);

        //연차 초기설정 및 시작
        YearManager.Init();
    }
}
