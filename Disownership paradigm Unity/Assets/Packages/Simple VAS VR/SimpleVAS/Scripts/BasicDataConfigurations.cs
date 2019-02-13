using System.Collections;
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
		public Toggle calibrationToggle, soundToggle, threatCueToggle, finishOnDurationToggle;//added soundToggle and threatCueToggle
		//added
		public Dropdown webcamDevice, serialDropdown;
		public static string ID, age, gender, handedness, conditionOrder;
		public static bool useCalibration, useSound, useThreatCue, finishOnduration;//added useSound & useThreatCue
		//added
		public static int selectedWebcamDevice;
		public static string selectedSerialPort;
		public static float conditionDuration, threatTime;

		// Use this for initialization
		void Start () {
			nextButton.interactable = false;
			//SetSerialDropDownOptions ();

			//added soundToggle stuff
			if (PlayerPrefs.GetInt ("use sound") == 1)	soundToggle.isOn = true;
			else	soundToggle.isOn = false;

			if (PlayerPrefs.GetInt ("use threat") == 1)	threatCueToggle.isOn = true;
			else	threatCueToggle.isOn = false;

            if (PlayerPrefs.GetInt("finish on duration") == 1) finishOnDurationToggle.isOn = true;
            else finishOnDurationToggle.isOn = false;

			conditionDurationField.text = PlayerPrefs.GetFloat("condition duration").ToString();
			threatTimeField.text = PlayerPrefs.GetFloat("threat time").ToString();
            
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
            finishOnduration = finishOnDurationToggle.isOn;

			//added
			selectedWebcamDevice = webcamDevice.value;
			selectedSerialPort = serialDropdown.GetComponentInChildren<Text> ().text;

			if (soundToggle.isOn)	useSound = true;
			else	useSound = false;
			if (threatCueToggle.isOn)	useThreatCue = true;
			else	useThreatCue = false;

			conditionDuration = float.Parse (conditionDurationField.text);
			threatTime = float.Parse (threatTimeField.text);

			storePreferences ();
		}

		//added
		private void storePreferences(){
			
			PlayerPrefs.SetInt ("serial port", serialDropdown.value);

			//added soundToggle stuff
			if (soundToggle.isOn) 	PlayerPrefs.SetInt ("use sound", 1);
			else PlayerPrefs.SetInt ("use sound", 0);

			if (threatCueToggle.isOn) 	PlayerPrefs.SetInt ("use threat", 1);
			else PlayerPrefs.SetInt ("use threat", 0);

            if (finishOnDurationToggle.isOn) PlayerPrefs.SetInt("finish on duration", 1);
            else PlayerPrefs.SetInt("finish on duration", 0);

            PlayerPrefs.SetFloat ("condition duration", conditionDuration);
			PlayerPrefs.SetFloat ("threat time", threatTime);



		}

		/*
		//added 
		public void SetSerialDropDownOptions () {

			serialDropdown.options.Clear ();

			List<string> ports = new List<string> ();
			foreach (string c in SerialPort.GetPortNames()){
				ports.Add (c);
			}

			serialDropdown.AddOptions (ports);
			serialDropdown.value = PlayerPrefs.GetInt ("serial port");
		}*/

	}

}