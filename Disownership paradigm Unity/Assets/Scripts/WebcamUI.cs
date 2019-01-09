using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamUI : MonoBehaviour {

	public RawImage display;
	public Dropdown webcamSelector;

	private WebCamTexture webcamTexture;
	//private int webcamSelectorNumber;

	// Use this for initialization
	void Start () {

		webcamSelector.value = PlayerPrefs.GetInt ("selectedCamera");
		SelectWebcam ();
	}
	

	public void SelectWebcam () {

		if(webcamTexture != null) webcamTexture.Stop();

		WebCamDevice[] devices = WebCamTexture.devices;
		string deviceName = devices[webcamSelector.value].name;
		webcamTexture = new WebCamTexture(deviceName);

		display.texture = webcamTexture;

		webcamTexture.Play();
	}


	void OnDestroy(){
		PlayerPrefs.SetInt ("selectedCamera", webcamSelector.value);
		if(webcamTexture != null) webcamTexture.Stop();

	}
}
