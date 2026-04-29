using UnityEngine;
using UnityEngine.SceneManagement;

public class CapsuleCollisionSensor : MonoBehaviour
{
    public string capsuleName;            // "LEFT", "RIGHT", "UP", "DOWN", vs.
    public string wireTag = "Wire";

    private Renderer rend;
    private Color originalColor;
    private bool isTouching = false;

    private int vibrationStrength;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;

        // Sahneler bazında titreşim şiddetini ayarla
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scene2") vibrationStrength = 120;
        else if (sceneName == "Scene3") vibrationStrength = 200;
        else vibrationStrength = 150;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(wireTag))
        {
            isTouching = true;
            string motorDir = VibrationDirectionManager.Instance.GetMotorDirection(capsuleName);
            if (!string.IsNullOrEmpty(motorDir))
            {
                BluetoothManager.Instance.SendCommand($"{motorDir}:{vibrationStrength}");
                Debug.Log($"🟢 {capsuleName} değdi → {motorDir}:{vibrationStrength}");
                if (rend != null) rend.material.color = Color.green;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(wireTag) && isTouching)
        {
            isTouching = false;
            string motorDir = VibrationDirectionManager.Instance.GetMotorDirection(capsuleName);
            if (!string.IsNullOrEmpty(motorDir))
            {
                BluetoothManager.Instance.SendCommand($"{motorDir}:0");
                Debug.Log($"⚪ {capsuleName} ayrıldı → {motorDir}:0");
                if (rend != null) rend.material.color = originalColor;
            }
        }
    }
}
