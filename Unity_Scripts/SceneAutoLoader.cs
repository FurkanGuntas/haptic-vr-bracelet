using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Scene2"; // Stage name to be skipped

    void Start()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
