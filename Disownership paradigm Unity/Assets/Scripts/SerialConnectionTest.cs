using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class SerialConnectionTest : MonoBehaviour {

    [HideInInspector]
    public string portName;
    public string testMessage;
    private int baudRate = 9600;//Fixed to 9600 for the trigger box.

    public Dropdown serialDropdown;

    SerialPort serialDevice;

    private void Start()
    {
        if (testMessage == "")
            testMessage = "9";

        portName = serialDropdown.GetComponentInChildren<Text>().text;

        if (portName != "")
        {
            portName = portName.Remove(0, 3);

            int portNumber = int.Parse(portName);

            if (portNumber < 10)
                portName = "COM" + portNumber;
            else
                portName = "\\\\.\\" + "COM" + portNumber;
        }
    }

    public void OnButtonPressed()
    {
        if (serialDevice == null)
            StartConnection();

        SendTestMessage();
    }

    public void StartConnection()
    {
        if(serialDevice == null) { 
            serialDevice = new SerialPort(portName, baudRate); //initializes a serial port
            //if (serialDevice != null) serialDevice.Close(); //makes sure the device is closed before openning
            serialDevice.Open(); //opens serial device
        }
    }


    public void SendTestMessage()
    {
        if (serialDevice.IsOpen)
        {
            serialDevice.Write("9");
        }

    }

    void OnDisable()
    {
        serialDevice.Close(); //close device when finished.
    }


}
