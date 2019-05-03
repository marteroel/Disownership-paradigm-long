using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutStimulation : MonoBehaviour {

    private int count;
    private bool isActive;
    public GameObject panel;
    public static bool readyToFinish;

	// Use this for initialization
	void Start () {
        readyToFinish = false;
        panel.SetActive(false);
        isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
            InterruptStimulation();

	}

    private void InterruptStimulation()
    {
        count++;

        if (count <= 2) { 
            isActive = !isActive;
            panel.SetActive(isActive);
        }

        else
            SceneManager.LoadScene("ForcedChoice");

    }
}
