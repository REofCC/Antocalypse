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
    // ±«»Ò¡ÿ - spwanManager √ﬂ∞°
    static SpawnManager spawnManager = new();
    public static SpawnManager SpawnManager { get { return spawnManager; } }

    private void Start()
    {
        manager = GetComponent<Managers>();
        DontDestroyOnLoad(manager);
    }
}
