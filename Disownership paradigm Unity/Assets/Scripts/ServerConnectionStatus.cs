using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerConnectionStatus : MonoBehaviour {

    private TCPClient tcpClient;
    public Text connectionStatus;

    void Awake()
    {
        tcpClient = FindObjectOfType<TCPClient>();
    }

    // Use this for initialization
    void Start () {
      StartCoroutine(WaitForServerStatus());
	}

    private IEnumerator WaitForServerStatus() //Not the most elegant way
    {
        yield return new WaitForFixedTime(1f);//waits for one second to check for server status.

        if (tcpClient.isConnected)
        {
            connectionStatus.text = "connected";
            connectionStatus.color = Color.green;
        }

        else
        {
            connectionStatus.text = "disconnected";
            connectionStatus.color = Color.red;
        }

    }
}
