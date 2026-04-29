using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DirectionSensor
{
    public Transform capsuleTransform;      // Her yön için bir kapsül objesi
    public string direction;                // LEFT, RIGHT, FORWARD, BACK, UP, DOWN
    public bool isVibrating = false;        // Durumu takip et
}

public class HandleCollisionDetector : MonoBehaviour
{
    public List<DirectionSensor> sensors;
    public float triggerDistance = 0.001f;
    public int vibrationIntensity = 150;

    GameObject[] wireObjects;

    void Start()
    {
        wireObjects = GameObject.FindGameObjectsWithTag("Wire");
    }

    void Update()
    {
        foreach (var sensor in sensors)
        {
            bool nearWire = false;

            foreach (var wire in wireObjects)
            {
                Collider wireCollider = wire.GetComponent<Collider>();
                if (wireCollider == null) continue;

                Vector3 closest = wireCollider.ClosestPoint(sensor.capsuleTransform.position);
                float dist = Vector3.Distance(sensor.capsuleTransform.position, closest);

                if (dist <= triggerDistance)
                {
                    nearWire = true;
                    if (!sensor.isVibrating)
                    {
                        BluetoothManager.Instance.SendCommand(sensor.direction + ":" + vibrationIntensity);
                        Debug.Log("🟢 Titreşim başladı: " + sensor.direction);
                        sensor.isVibrating = true;
                    }
                    break;
                }
            }

            if (!nearWire && sensor.isVibrating)
            {
                BluetoothManager.Instance.SendCommand(sensor.direction + ":0");
                Debug.Log("⚪ Titreşim durdu: " + sensor.direction);
                sensor.isVibrating = false;
            }
        }
    }
}
