using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZoneLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandleCapsule"))
        {
            Debug.Log("The EndSphere has been touched. Moving to Scene 3.");
            SceneManager.LoadScene("Scene3");
        }
    }
}
