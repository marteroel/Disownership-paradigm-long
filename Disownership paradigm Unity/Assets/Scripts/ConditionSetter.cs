using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionSetter : MonoBehaviour {

	public Text[] eachCondition;
	public InputField[] delayPerCondition;

	public static List<string> selectedConditionOrder = new List<string>();
	public static List<float> selectedDelayOrder = new List<float>();


	public void OnNextButtonPressed () {


		for (int i = 0; i < eachCondition.Length; i++) {

			selectedConditionOrder.Add(eachCondition [i].text);
					
			if (delayPerCondition[i].text != "")
				selectedDelayOrder.Add (float.Parse (delayPerCondition [i].text));
			else
				selectedDelayOrder.Add (0f);

			//Debug.Log(selectedDelayOrder[i]);
		}
			
	}
}
