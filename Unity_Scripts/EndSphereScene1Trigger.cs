using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSphereScene1Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandleCapsule"))
        {
            Debug.Log("🏁 EndSphere'e temas edildi. Scene2'ye geçiliyor.");
            SceneManager.LoadScene("Scene2");
        }
    }
}
