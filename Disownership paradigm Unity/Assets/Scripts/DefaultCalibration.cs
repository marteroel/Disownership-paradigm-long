using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class DefaultCalibration : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (BasicDataConfigurations.useCalibration)
            UnityEngine.XR.InputTracking.Recenter();
    }
	
}
