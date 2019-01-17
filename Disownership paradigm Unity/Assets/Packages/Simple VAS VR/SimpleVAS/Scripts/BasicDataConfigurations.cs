﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.IO.Ports;

namespace SimpleVAS {
	public class BasicDataConfigurations : MonoBehaviour {

		public InputField nameField, ageField, conditionDurationField, threatTimeField;//added  conditionDurationField, threatTimeField
		public Text genderField, handednessField;
		public Button nextButton;
		public Toggle calibrationToggle, soundToggle;//added soundToggle
		//added
		public Dropdown webcamDevice, serialDropdown;
		public static string ID, age, gender, handedness, conditionOrder;
		public static bool useCalibration, useSound;//added useSound
		//added
		public static int selectedWebcamDevice;
		public static string selectedSerialPort;
		public static float conditionDuration, threatTime;

		// Use this for initialization
		void Start () {
			nextButton.interactable = false;
			SetSerialDropDownOptions ();

			//added soundToggle stuff
			if (PlayerPrefs.GetInt ("use sound") == 1)
				soundToggle.isOn = true;
			else
				soundToggle.isOn = false;

			if(PlayerPrefs.GetFloat("condition duration") != null)
				conditionDurationField.text = PlayerPrefs.GetFloat("condition duration").ToString();

			if(PlayerPrefs.GetFloat("threat time") != null)
				conditionDurationField.text = PlayerPrefs.GetFloat("threat time").ToString();
		}
		
		// Update is called once per frame
		void Update () {
			if (ID != null && age != null)
				nextButton.interactable = true;
		}

		public void userName() {
			ID = nameField.text;
		}

		public void userAge() {
			age = ageField.text;
		}

		public void OnNextButton () {

			gender = genderField.text;
			handedness = handednessField.text;
			useCalibration = calibrationToggle.isOn;

			//added
			selectedWebcamDevice = webcamDevice.value;
			if (soundToggle.isOn)	useSound = true;
			else	useSound = false;
			conditionDuration = float.Parse (conditionDurationField.text);
			threatTime = float.Parse (threatTimeField.text);


			selectedSerialPort = serialDropdown.GetComponentInChildren<Text> ().text;
		}

		//added
		private void storePreferences(){
			
			PlayerPrefs.SetInt ("serial port", serialDropdown.value);

			//added soundToggle stuff
			if (soundToggle.isOn) 	PlayerPrefs.SetInt ("use sound", 1);
			else PlayerPrefs.SetInt ("use sound", 0);

			PlayerPrefs.SetFloat ("condition duration", conditionDuration);
			PlayerPrefs.SetFloat ("threat time", threatTime);

		}

		//added 
		public void SetSerialDropDownOptions () {

			serialDropdown.options.Clear ();

			List<string> ports = new List<string> ();
			foreach (string c in SerialPort.GetPortNames()){
				ports.Add (c);
			}

			serialDropdown.AddOptions (ports);
			serialDropdown.value = PlayerPrefs.GetInt ("serial port");
		}

	}

}