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
        string ardityPortFormatted;

        string serialPort = serialDropdown.GetComponentInChildren<Text>().text;

        if (serialPort != "")
        {    
            serialPort = serialPort.Remove(0, 3);

            int portNumber = int.Parse(serialPort);

            if (portNumber < 10)
                ardityPortFormatted = "COM" + portNumber;
            else
                ardityPortFormatted = "\\\\.\\" + "COM" + portNumber;


        }
        else ardityPortFormatted = "";

        return ardityPortFormatted;
    }
}
