using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OscMessageManager : MonoBehaviour {

	public OSCTransmitter Transmitter;
	public string address = "/sound/";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSendMessage (string messageString) {

		var message = new OSCMessage(address);
		//message.AddValue(OSCValue.String(messageString));

		Transmitter.Send(message);

	}
}
