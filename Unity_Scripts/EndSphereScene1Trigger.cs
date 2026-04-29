using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSphereScene1Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandleCapsule"))
        {
            Debug.Log("The EndSphere has been touched. Moving to Scene 2.");
            SceneManager.LoadScene("Scene2");
        }
    }
}
