using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionSetter : MonoBehaviour {

	public Text[] eachCondition;
	public InputField[] delayPerCondition;
    public float defaultDelayTime;

	public static List<string> selectedConditionOrder = new List<string>();
	public static List<float> selectedDelayOrder = new List<float>();


	public void OnNextButtonPressed () {


		for (int i = 0; i < eachCondition.Length; i++) {

			selectedConditionOrder.Add(eachCondition [i].text);

			if (delayPerCondition[i].text != "")
				selectedDelayOrder.Add (float.Parse (delayPerCondition [i].text));
			else //if (delayPerCondition[i].placeholder != "")
				selectedDelayOrder.Add (defaultDelayTime);//if empty adds delay of 1 by default
		}
			
	}
}
