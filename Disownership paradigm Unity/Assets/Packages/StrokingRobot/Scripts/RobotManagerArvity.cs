using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleVAS;

namespace StrokingRobot {
	public class RobotManagerArvity : MonoBehaviour { //changed class to use with Arvity for better handling serial messages than own solution.

		public SerialController serialManager;

		[Range(0,100)]
		public int distance;
		[Range(2,16)]
		public int speed;
		[Range(0,90)]
		public int angle;

		[HideInInspector]
		public float totalTime;

		[HideInInspector]
		public bool readyToReceiveCommands = false; 

		[HideInInspector]
		public bool acknowledgedInstruction = false;

		private bool isReady = false;
		private string currentIncomingMessage, response;

		//SEND MOVEMENT SEGMENT SHOULD BE SENT N NUMBER OF TIMES, IN ORDER TO SUM 100 UNITS OF DISTANCE.
		//AFTER ALL THE SEGMENTS ARE SENT, THE STARTMOVEMENT() SHOULD BE CALLED
		//CLEAR NUMBER SEGMENT SHOULD BE CALLED BEFORE APPLYING A NEW SET OF MOVEMENT SEGMENTS.

		// Invoked when a line of data is received from the serial device.
		void OnMessageArrived(string msg)
		{

			currentIncomingMessage = msg;

			if(msg.Substring(0,1) != "L") //this is used to not debug every step of the robot but only more relevant messages.
				Debug.Log("Message arrived: " + msg);
			
			if (msg.Substring (0, 1) == "T")
				UseStrokerTime (msg);
			else if (msg == "ACK")
				isReady = true;
			else if (msg == "ERR")
				isReady = false;

			if (msg == "Ready to receive commands or start execution") {
				Debug.Log ("Robot is ready");
				readyToReceiveCommands = true;
			}
		}

        void Awake()
        {
            serialManager.portName = BasicDataConfigurations.selectedSerialPort;
        }


        void Update(){
		}

		//Deals with the Time response in the format: T:2.50,P:1.50,1.00 -> total time (T:) 2.50, time per segment (P:) 1.50, 1...
		void UseStrokerTime(string msg){
			
				msg = msg.Replace("T:", "");//removes T:
				msg = msg.Replace("P:", "");//removes P:

				List<string> segmentTimes = msg.Split (',').ToList<string>();//makes into list separated by commas

				totalTime = float.Parse(segmentTimes [0]);//first item on the list is total time

				for (int i = 1; i < segmentTimes.Count; i++) {
					//do something for each segment time.
					//Debug.Log ("The segment " + i + " " + segmentTimes[i]);
				}

		}

		// Invoked when a connect/disconnect event occurs. The parameter 'success' will be 'true' upon connection, and 'false' upon disconnection or
		// failure to connect.
		void OnConnectionEvent(bool success)
		{
			if (success) {
				Debug.Log ("Connection established");
				StartCoroutine(InitializeValuesWhenReady ());
			}
			else
				Debug.Log("Connection attempt failed or disconnection detected");
		}

		//Waits for ACK message before initializing.
		private IEnumerator InitializeValuesWhenReady(){
			while (!isReady) {
				yield return null;
			}

			SendMovementSegment (distance, speed, angle);
			yield return null;
		}


		public void SendMovementSegment(int distance, int speed, int angle){

			string message = ("SET,L" + distance.ToString("000") + ",MS" + speed.ToString("00") + ",SV" + angle.ToString("00") + ",R" + speed.ToString("00")); //("00") sets the number of digits to 2

			if (distance < 0 || distance > 100)
				Debug.LogError ("wrong distance value, should be between 0 and 100");
			else if (speed < 2 || speed > 16)
				Debug.LogError ("wrong speed value, should be between 2 and 16");
			else if (angle < 0 || angle > 180)
				Debug.LogError ("wrong angle value, should be between 0 and 180");


			else {
				serialManager.SendSerialMessage (message);
				StartCoroutine (WaitForResponse ());
			}
		}

		public void ClearNumberSegment(){
			totalTime = 0;//resets total time to cero
			serialManager.SendSerialMessage ("CLEAR");
			StartCoroutine (WaitForResponse ());

//			while (currentIncomingMessage != "ACK") {
				
//			}

		}

		public void StartMovement(){
			serialManager.SendSerialMessage ("START");
			StartCoroutine (WaitForResponse ());
			readyToReceiveCommands = false;
//			Debug.Log ("robot is still moving");
		}

		public void ReadSegment(){
			serialManager.SendSerialMessage ("READ");
			StartCoroutine (WaitForResponse ());
		}

		public void CallDuration(){
			serialManager.SendSerialMessage ("TIME");
			StartCoroutine (WaitForResponse ());
		}

		public float ReadDuration(){
			return totalTime;
		}

		public bool IsReadyToStartMovement() {
			return readyToReceiveCommands;
			StartCoroutine (WaitForResponse ());
		}

		public IEnumerator WaitForResponse(){
			
			acknowledgedInstruction = false;
			response = "";


			while (response != "ACK") {
				response = currentIncomingMessage;
				yield return null;
			}

			acknowledgedInstruction = true;
			Debug.Log ("Acknowledged your instruction");
			yield return null;
		}


	}
}
