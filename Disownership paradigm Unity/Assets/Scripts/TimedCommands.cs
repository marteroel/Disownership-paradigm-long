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

    public bool sendOSCMessage;

	private SerialControl serialController;
	public OscMessageManager oscMessageManager;
	private string threatMessage;

    public static TimedCommands instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
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
		if (ConditionSetter.selectedConditionOrder[QuestionManager.currentCondition] == "active") {
			if (ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition] == 0f)
				threatMessage = "1";
			else
				threatMessage = "2";
		} 

		else if (ConditionSetter.selectedConditionOrder[QuestionManager.currentCondition] == "passive") {
			if (ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition] == 0f)
				threatMessage = "3";
			else
				threatMessage = "4";
		}


	}

	//private IEnumerator WaitBeforeShowingCameraFeed(){
	//	yield return new WaitForFixedTime (1f);
	//}

	private IEnumerator TriggerStimulationAt(){

		Debug.Log ("should trigger this coroutine");
		yield return new WaitForFixedTime (timeForThreat);

		serialController.WriteToPort(threatMessage);
        stimulationCue.GetComponent<Image>().color = stimulationCueColor;

        if (sendOSCMessage)
            oscMessageManager.OnSendMessage("1");//sends osc message if needed to trigger external application.

	}

	private IEnumerator LoadSceneAt(){

		yield return new WaitForFixedTime (sceneDuration);
		SceneManager.LoadScene (sceneToLoad);
        fadePanel.SetActive(true);
    }


}
