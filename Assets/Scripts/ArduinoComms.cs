using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.IO;

public class ArduinoComms : MonoBehaviour
{
    public SerialPort stream = new SerialPort("COM3",9600);
    public bool arduinoConnected;

    // Start is called before the first frame update
    void Start()
    {
        arduinoConnected = true;
        try
        {
        stream.Open();
        }
        catch(IOException)
        {
            Debug.Log("Arduino not connected");
            arduinoConnected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
