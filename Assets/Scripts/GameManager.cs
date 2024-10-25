using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Manager { get { return instance; } }

    static ColonyManager _colony = new();
    public static ColonyManager Colony { get { return _colony; } }

    static TaskManager _task = new();
    public static TaskManager Task { get { return _task; } }


    private void Start()
    {
        Init();
        _task.OnStart();
    }
    void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
            }
            if (go.GetComponent<GameManager>() == null)
            {
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<GameManager>();
        }
    }
}
