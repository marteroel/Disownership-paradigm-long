using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class StimulationTestSettings : MonoBehaviour {

	public WebcamDisplay webcamDisplay;
	public bool useManualSettings;
	public int manualWebcamIndex;

	// Use this for initialization
	void Start () {
		if (useManualSettings) 
			webcamDisplay.SetWebCam (manualWebcamIndex);

		else 
			webcamDisplay.SetWebCam (BasicDataConfigurations.selectedWebcamDevice);
		
	}

}
