using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoconnectToServer : MonoBehaviour {

    private TCPClient tcpClient;
    public Text connectionStatus;

    void Awake() {
        tcpClient = GetComponent<TCPClient>();
    }

    private void Start()
    {
        connectionStatus.text = "attempting connection...";
        SetConnection();
    }

    public void SetConnection()
    {
        tcpClient.ip = PlayerPrefs.GetString("server_ip");
        tcpClient.ConnectToTcpServer();
        StartCoroutine(WaitForServerStatus());

    }

    private IEnumerator WaitForServerStatus() //Not the most elegant way
    {
        yield return new WaitForFixedTime(3f);//waits for one second to check for server status.

        if (tcpClient.isConnected)
        {
            connectionStatus.text = "connected";
            connectionStatus.color = Color.green;
        }

        else {
            connectionStatus.text = "disconnected";
            connectionStatus.color = Color.red;
        }

    }
}
