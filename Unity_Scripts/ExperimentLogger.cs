using System.IO;
using UnityEngine;

public class ExperimentLogger : MonoBehaviour
{
    public static ExperimentLogger Instance { get; private set; }

    public int participantNumber;
    private string filePath;
    private StreamWriter file;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string fileName = $"participant_{participantNumber}.csv";
        filePath = Path.Combine(desktopPath, fileName);

        bool fileExists = File.Exists(filePath);
        file = new StreamWriter(filePath, true);

        if (!fileExists)
            file.WriteLine("Scene;Participant;Number of Hits;Time Elapsed (s)");
    }

    public void SaveSceneData(string sceneName, int collisionCount, float elapsedTime)
    {
        if (file != null)
        {
            file.WriteLine($"{sceneName};{participantNumber};{collisionCount};{elapsedTime:F2}");
            file.Flush();
            Debug.Log($"📁 Kaydedildi: {sceneName};{participantNumber};{collisionCount};{elapsedTime:F2}");
        }
    }

    public void CloseLogFile()
    {
        if (file != null)
        {
            file.Close();
            file = null;
            Debug.Log("📁 Log dosyası başarıyla kapatıldı.");
        }
    }

    void OnDestroy()
    {
        file?.Close();
    }
}
