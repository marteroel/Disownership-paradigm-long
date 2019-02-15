using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace SimpleVAS {
		public class Calibrator : MonoBehaviour {

        //changed from Start to Awake
        //void Start(){
        void Awake()
        {
			if (BasicDataConfigurations.useCalibration)
				InputTracking.disablePositionalTracking = true;
		}

		void Update () {

			if(BasicDataConfigurations.useCalibration){
				if (Input.GetKeyDown("c")){
						UnityEngine.XR.InputTracking.Recenter();	
				}
			}
		}
	}
}
