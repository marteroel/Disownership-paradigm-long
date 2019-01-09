using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class ConditionInstructions : MonoBehaviour {

	public GameObject instructionsSelf, instructionsOther;

	// Use this for initialization
	void Start () {
		if (ConditionSetter.selectedConditionOrder [QuestionManager.currentCondition] == "self")
			instructionsSelf.SetActive(true);
		else 
			instructionsOther.SetActive(true);
		
	}
}
