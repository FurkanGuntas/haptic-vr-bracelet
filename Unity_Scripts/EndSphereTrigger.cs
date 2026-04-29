using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSphereTrigger : MonoBehaviour
{
    private bool hasLogged = false; // It tracks whether registration has been completed.
    public GameObject mazeObject;   

    private void OnTriggerEnter(Collider other)
    {
        if (hasLogged) return;

        if (other.CompareTag("HandleCapsule"))
        {
            hasLogged = true;

            string currentScene = SceneManager.GetActiveScene().name;
            var handleTimer = other.GetComponentInParent<HandleTimer>();
            var collisionTracker = other.GetComponentInParent<CollisionTracker>();

            if (handleTimer != null && collisionTracker != null)
            {
                ExperimentLogger.Instance?.SaveSceneData(currentScene, collisionTracker.CollisionCount, handleTimer.TimeElapsed);
                Debug.Log($" Data for {currentScene} has been saved.");
            }

            if (mazeObject != null)
            {
                mazeObject.SetActive(false); 
                Debug.Log("Maze has been disabled..");
            }

            if (currentScene == "Scene3")
            {
                ExperimentLogger.Instance?.CloseLogFile();
                Debug.Log("Scene3 data has been saved, the experiment is complete.");
            }
        }
    }
}
