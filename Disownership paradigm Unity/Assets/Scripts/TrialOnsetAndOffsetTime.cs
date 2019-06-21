using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrialOnsetAndOffsetTime : MonoBehaviour {

    public static float onsetTime, offsetTime;

	// Use this for initialization
	void Start () {

        GetTrialOnset();
        SceneManager.sceneUnloaded += GetTrialOffset;

	}

    public void GetTrialOnset()
    {
        onsetTime = Time.realtimeSinceStartup;
        //Debug.Log("onset time is " + onsetTime);
    }

    private void GetTrialOffset(Scene scene)
    {
        offsetTime = Time.realtimeSinceStartup;
        //Debug.Log("offset time is " + offsetTime);
        SceneManager.sceneUnloaded -= GetTrialOffset;
    }
}
