using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZoneLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandleCapsule"))
        {
            Debug.Log("🏁 EndSphere'e temas edildi. Scene3'e geçiliyor.");
            SceneManager.LoadScene("Scene3");
        }
    }
}
