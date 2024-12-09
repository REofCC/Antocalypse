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
    // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ - spwanManager ï¿½ß°ï¿½
    static SpawnManager spawnManager = new();
    public static SpawnManager SpawnManager { get { return spawnManager; } }
    // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ - yearManager ï¿½ß°ï¿½
    static YearManager yearManager;
    public static YearManager YearManager { get { return yearManager; } }

    private void Awake()
    {
        yearManager = gameObject.AddComponent<YearManager>();        
    }
#region TaskManager ï¿½ß°ï¿½ - ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
    static TaskManager _task = new();
    public static TaskManager Task { get { return _task; } }
    #endregion

    private void Start()
    {
        // ±ÇÈñÁØ - task ÃÊ±âÈ­ ¹× ÄÚ·çÆ¾ »ç¿ëÀ» À§ÇØ GameObject¿¡ Ãß°¡
        _task = gameObject.AddComponent<TaskManager>();
        _task.Init();
        manager = GetComponent<Managers>();
        DontDestroyOnLoad(manager);

        //ï¿½ï¿½ï¿½ï¿½ ï¿½Ê±â¼³ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        yearManager.Init();
    }
}
