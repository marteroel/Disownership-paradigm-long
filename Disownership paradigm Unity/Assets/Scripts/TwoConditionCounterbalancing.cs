using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoConditionCounterbalancing : MonoBehaviour {

    public InputField[] delayPerCondition;
    public Toggle orderToggle;
    private float[] order1 = { 0f, 0.9f, 0f, 1f, 0f, 1f };
    private float[] order2 = { 0.9f, 0f, 1f, 0f, 1f, 0f };

    public void SetConditionOrder() {

        Debug.Log("setting condition order");

        float[] selectedOrderArray;

        if (!orderToggle.isOn)
            selectedOrderArray = order1;
        else
            selectedOrderArray = order2;

        for (int i = 0; i < delayPerCondition.Length; i++) {
            Debug.Log(selectedOrderArray[i]);
            delayPerCondition[i].text = selectedOrderArray[i].ToString();
            
        }
    }
}
