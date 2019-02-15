using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;

public class ConditionalStimulationSceneSelector : MonoBehaviour {

    public LoadScene sceneLoader;
    public string sceneWithoutEmbeddedVAS;
    public string sceneWithEmbeddedVAS;

    // Use this for initialization
    void Start () {
        if (BasicDataConfigurations.embedVAS)
            sceneLoader.sceneToLoad = sceneWithEmbeddedVAS;
        else
            sceneLoader.sceneToLoad = sceneWithoutEmbeddedVAS;
	}
	
}
