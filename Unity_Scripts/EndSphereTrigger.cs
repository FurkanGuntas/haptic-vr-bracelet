using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSphereTrigger : MonoBehaviour
{
    private bool hasLogged = false; // Kayıt yapılıp yapılmadığını takip eder.
    public GameObject mazeObject;   // Maze11 objesini burada referans göstereceğiz.

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
                Debug.Log($"✅ {currentScene} verileri kaydedildi.");
            }

            if (mazeObject != null)
            {
                mazeObject.SetActive(false); // Maze11 objesini devre dışı bırak
                Debug.Log("🚫 Maze11 devre dışı bırakıldı.");
            }

            if (currentScene == "Scene3")
            {
                ExperimentLogger.Instance?.CloseLogFile();
                Debug.Log("✅ Scene3 verileri kaydedildi, deney tamamlandı.");
            }
        }
    }
}