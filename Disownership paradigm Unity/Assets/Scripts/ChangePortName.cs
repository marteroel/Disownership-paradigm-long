using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePortName : MonoBehaviour {

    public Dropdown serialDropdown;


    public static ChangePortName instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
            Debug.Log("\\" +".\"");
	}

    public string ardityFormattedPort()
    {
        Debug.Log("this is activated");
        string serialPort = serialDropdown.GetComponentInChildren<Text>().text;
        serialPort = serialPort.Remove(0, 3);
        Debug.Log(serialPort + " this is the number of the com");

        int portNumber = int.Parse(serialPort);

        string ardityPortFormatted;

        if (portNumber < 10)
            ardityPortFormatted = "COM" + portNumber;
        else
            ardityPortFormatted = "\\\\.\\" + "COM" + portNumber;

        return ardityPortFormatted;
    }
}
