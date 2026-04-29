using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Scene2"; // Geçilecek sahne adı

    void Start()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
