using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class ConditionInstructions : MonoBehaviour {

	public GameObject instructionsA, instructionsB;

	// Use this for initialization
	void Start () {
		if (ConditionSetter.selectedConditionOrder [QuestionManager.currentCondition] == "active")
			instructionsA.SetActive(true);
		else 
			instructionsB.SetActive(true);
		
	}
}
