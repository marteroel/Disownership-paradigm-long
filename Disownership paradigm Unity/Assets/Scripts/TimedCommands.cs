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

	//private SerialControl serialController; //COMMENTED OUT FOR SERIAL ROBOT
    private TCPClient tcpCommunicator;

    public OscMessageManager oscMessageManager;
	private string threatMessage;

    public static TimedCommands instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        tcpCommunicator = FindObjectOfType<TCPClient>();
    }
    // Use this for initialization
    void Start () {

        fadePanel.SetActive(false);
		//serialController = GetComponent<SerialControl> (); //COMMENTED OUT FOR SERIAL ROBOT

        StartCoroutine("TriggerStimulationAt");

       // if(BasicDataConfigurations.finishOnduration)
		 //   StartCoroutine("LoadSceneAt");	//commented out to trigger once robot has began movements.

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

        //serialController.WriteToPort(threatMessage);  //COMMENTED OUT FOR SERIAL ROBOT
        oscMessageManager.OnSendMessage("1");//sends osc message if needed to trigger external application.
		stimulationCue.GetComponent<Image>().color = stimulationCueColor;
	}

    public void StartLoadSceneCoroutine()
    {
        StartCoroutine(LoadSceneAt());
    }

	private IEnumerator LoadSceneAt(){//changed to public to trigger from robot scripts
        yield return new WaitForFixedTime (sceneDuration);
        tcpCommunicator.SendTCPMessage("end block " + QuestionManager.currentCondition.ToString());
        SceneManager.LoadScene (sceneToLoad);
        fadePanel.SetActive(true);
        StopAllCoroutines();
    }


}
