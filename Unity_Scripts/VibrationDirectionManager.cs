using System.Collections.Generic;
using UnityEngine;

public class VibrationDirectionManager : MonoBehaviour
{
    public static VibrationDirectionManager Instance;
    private Dictionary<string, string> capsuleToMotorMap = new Dictionary<string, string>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetDirectionMap(Dictionary<string, string> newMap)
    {
        capsuleToMotorMap = newMap;
    }

    public string GetMotorDirection(string capsuleName)
    {
        return capsuleToMotorMap.TryGetValue(capsuleName, out var dir) ? dir : null;
    }
}
