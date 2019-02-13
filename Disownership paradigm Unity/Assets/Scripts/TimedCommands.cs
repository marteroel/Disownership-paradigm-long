using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleVAS;
using UnityEngine.UI;

public class TimedCommands : MonoBehaviour {

	public float timeForThreat, sceneDuration;
	public string sceneToLoad;
	public GameObject stimulationCue;
	public Color stimulationCueColor;
    public GameObject fadePanel;

	private SerialControl serialController;
	public OscMessageManager oscMessageManager;
	private string threatMessage;

	// Use this for initialization
	void Start () {

        fadePanel.SetActive(false);
		serialController = GetComponent<SerialControl> ();

		StartCoroutine("TriggerStimulationAt");

        if(BasicDataConfigurations.finishOnduration)
		    StartCoroutine("LoadSceneAt");	

		if (BasicDataConfigurations.useThreatCue)	stimulationCue.SetActive (true);
		else	stimulationCue.SetActive (false);

		//sends serial messages for marking physiological recording.
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
		oscMessageManager.OnSendMessage("1");//sends osc message if needed to trigger external application.
		stimulationCue.GetComponent<Image>().color = stimulationCueColor;
	}

	private IEnumerator LoadSceneAt(){

		yield return new WaitForFixedTime (sceneDuration);
		SceneManager.LoadScene (sceneToLoad);
        fadePanel.SetActive(true);
    }


}
