using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderForStimulation : MonoBehaviour {

    public static SceneLoaderForStimulation instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void LoadScene(string scene)
    {

    }
}
