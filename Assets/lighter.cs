using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lighter : MonoBehaviour
{
    GameObject Connection;
    public int r = 1;
    void Start()
    {
        Connection= GameObject.Find("Main Camera");
    }

    
    void Update()
    {
        if (Connection.GetComponent<ArduinoComms>().arduinoConnected == true)
        Connection.GetComponent<ArduinoComms>().stream.WriteLine(r.ToString());
    }
}
