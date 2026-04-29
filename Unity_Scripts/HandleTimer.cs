using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleTimer : MonoBehaviour
{
    public string startTag = "StartSphere";
    public string endTag = "EndSphere";
    public CollisionTracker collisionTracker;

    private float timer = 0f;
    private bool isRunning = false;
    public float TimeElapsed => timer;
    private bool hasEnded = false;


    void Update()
    {
        if (isRunning)
            timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasEnded) return;

        if (other.CompareTag(startTag) && !isRunning)
        {
            StartTimer();
        }
        else if (other.CompareTag(endTag) && isRunning)
        {
            StopTimer();
            SaveAndHandleScene();
        }
    }

    void StartTimer()
    {
        timer = 0f;
        isRunning = true;
        Debug.Log("The timer has started.");
    }

    void StopTimer()
    {
        isRunning = false;
        Debug.Log("Timer stopped: " + timer.ToString("F2") + " second");
    }

    void SaveAndHandleScene()
    {
        hasEnded = true;
        string currentScene = SceneManager.GetActiveScene().name;

        if (ExperimentLogger.Instance != null)
        {
            ExperimentLogger.Instance.SaveSceneData(currentScene, collisionTracker.CollisionCount, timer);
        }

        if (currentScene == "SampleScene")
        {
            SceneManager.LoadScene("Scene2");
        }
        else if (currentScene == "Scene2")
        {
            SceneManager.LoadScene("Scene3");
        }
        else if (currentScene == "Scene3")
        {
            Debug.Log("Last scene completed.");
            // No scene transitions are made; only data recording and logging are left.
        }
    }
}
