using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleVAS;

public class TimedCommands : MonoBehaviour {

	public float timeForThreat, sceneDuration;
	public string sceneToLoad;

	private SerialControl serialController;
	public OscMessageManager oscMessageManager;
	private string threatMessage;

	// Use this for initialization
	void Start () {

		if(BasicDataConfigurations.threatTime != 0)
			timeForThreat = BasicDataConfigurations.threatTime;
		if(BasicDataConfigurations.conditionDuration != 0)
			sceneDuration = BasicDataConfigurations.conditionDuration;

		serialController = GetComponent<SerialControl> ();

		StartCoroutine("TriggerStimulationAt");
		StartCoroutine("LoadSceneAt");	


		if (ConditionSetter.selectedConditionOrder[QuestionManager.currentCondition] == "self") {
			if (ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition] == 0)
				threatMessage = "1";
			else
				threatMessage = "2";
		} 

		else if (ConditionSetter.selectedConditionOrder[QuestionManager.currentCondition] == "other") {
			if (ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition] == 0)
				threatMessage = "3";
			else
				threatMessage = "4";
		}

	}

	private IEnumerator TriggerStimulationAt(){
		
		yield return new WaitForFixedTime (timeForThreat);

		serialController.WriteToPort(threatMessage);
		oscMessageManager.OnSendMessage("1");
	}

	private IEnumerator LoadSceneAt(){

		yield return new WaitForFixedTime (sceneDuration);
		SceneManager.LoadScene (sceneToLoad);
	}


}
