using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class ConditionManager : MonoBehaviour {

	public WebcamDisplay webcamDisplay;
	public SerialControl serialController;
	public TimedCommands timedCommands;

	public bool useManualSettings;
	public float manualDelayTime;
	public int manualWebcamIndex;
	public string manualSerialPort;

	public float manualThreatDuration;
	public float manualSceneDuration;

	// Use this for initialization
	void Awake () {

		if (useManualSettings){
			webcamDisplay.delayTimeSeconds = manualDelayTime;
			serialController.portName = manualSerialPort;

			timedCommands.timeForThreat = manualThreatDuration;
			timedCommands.sceneDuration = manualSceneDuration;
		}

		else {
			webcamDisplay.delayTimeSeconds = ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition];	
			serialController.portName = BasicDataConfigurations.selectedSerialPort;

			//timed commands
			if (BasicDataConfigurations.threatTime != 0)
				timedCommands.timeForThreat = BasicDataConfigurations.threatTime;
			else
				timedCommands.timeForThreat = manualThreatDuration;

			if (BasicDataConfigurations.conditionDuration != 0)
				timedCommands.sceneDuration = BasicDataConfigurations.conditionDuration;
			else
				timedCommands.sceneDuration = manualSceneDuration;
		}
	}

	void Start() {
		
		if (useManualSettings) 
			webcamDisplay.SetWebCam (manualWebcamIndex);

		else 
			webcamDisplay.SetWebCam (BasicDataConfigurations.selectedWebcamDevice);

	}


	void Update () {
		if(useManualSettings){
			if (manualDelayTime != webcamDisplay.delayTimeSeconds)
				webcamDisplay.delayTimeSeconds = manualDelayTime; //sets delay on play
		}
	}
}
