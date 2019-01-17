using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class PlaySound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (BasicDataConfigurations.useSound)
			GetComponent<AudioSource> ().Play ();
	}
	

}
