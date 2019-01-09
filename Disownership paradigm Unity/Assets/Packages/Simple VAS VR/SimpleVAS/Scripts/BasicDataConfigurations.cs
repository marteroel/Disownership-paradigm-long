using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.IO.Ports;

namespace SimpleVAS {
	public class BasicDataConfigurations : MonoBehaviour {

		public InputField nameField, ageField;
		public Text genderField, handednessField;
		public Button nextButton;
		public Toggle calibrationToggle;
		//added
		public Dropdown webcamDevice, serialDropdown;
		public static string ID, age, gender, handedness, conditionOrder;
		public static bool useCalibration;
		//added
		public static int selectedWebcamDevice;
		public static string selectedSerialPort;

		// Use this for initialization
		void Start () {
			nextButton.interactable = false;
			SetSerialDropDownOptions ();
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

			PlayerPrefs.SetInt ("serial port", serialDropdown.value);
			selectedSerialPort = serialDropdown.GetComponentInChildren<Text> ().text;
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