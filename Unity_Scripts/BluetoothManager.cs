using System.IO.Ports;
using UnityEngine;

public class BluetoothManager : MonoBehaviour
{
    public string portName = "COM5";
    public int baudRate = 115200;
    private SerialPort serialPort;
    private static BluetoothManager instance;
    public static BluetoothManager Instance => instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            Debug.Log("Bluetooth connection established.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Bluetooth connection error: " + e.Message);
        }
    }

    public void SendCommand(string command)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.Write(command + "\n");
                Debug.Log("Sent: " + command);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Data transmission error: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}
