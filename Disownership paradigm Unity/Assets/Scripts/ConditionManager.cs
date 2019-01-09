using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class ConditionManager : MonoBehaviour {

	public WebcamDisplay webcamDisplay;
	public SerialControl serialController;

	// Use this for initialization
	void Awake () {
			webcamDisplay.delayTimeSeconds = ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition];	
			serialController.portName = BasicDataConfigurations.selectedSerialPort;
	}
}
