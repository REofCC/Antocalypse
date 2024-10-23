using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static ColonyManager Colony { get { return _colony; } }
    static ColonyManager _colony = new();
    public static TaskManager Task { get { return _task; } }
    static TaskManager _task = new();

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
