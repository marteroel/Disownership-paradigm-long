using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoConditionCounterbalancing : MonoBehaviour {

    public Dropdown[] delayPerCondition;
    public Toggle orderToggle;
    private int[] order1 = { 0, 1, 0, 1, 0, 1 };
    private int[] order2 = { 1, 0, 1, 0, 1, 0 };


    private void Start()
    {
        for (int i = 0; i < delayPerCondition.Length; i++)
            delayPerCondition[i].value = order1[i];
    }

    public void SetConditionOrder() {

        int[] selectedOrderArray;

        if (!orderToggle.isOn)
            selectedOrderArray = order1;
        else
            selectedOrderArray = order2;

        for (int i = 0; i < delayPerCondition.Length; i++) 
            delayPerCondition[i].value = selectedOrderArray[i];
 
    }
}
