using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public static TimeTracker Instance { get; private set; }

    private HandleTimer handleTimer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        handleTimer = FindObjectOfType<HandleTimer>();
    }

    public float GetElapsedTime()
    {
        if (handleTimer != null)
            return handleTimer.TimeElapsed;
        else
            return 0f;
    }
}
