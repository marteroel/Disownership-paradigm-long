using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class StartConditionAutomatically : MonoBehaviour {

	public LoadScene sceneLoader;
	public float time;

	// Use this for initialization
	void Start () {
		StartCoroutine (LoadConditionAtTime (time));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator LoadConditionAtTime (float time){
		yield return new WaitForFixedTime (time);
		sceneLoader.OnNextButton ();

	}
}
