using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnToggleAutomatically : MonoBehaviour {

	// Use this for initialization

	void Start () {
        this.GetComponent<Toggle>().isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
