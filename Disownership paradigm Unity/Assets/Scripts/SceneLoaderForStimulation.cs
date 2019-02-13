using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderForStimulation : MonoBehaviour {

    public static SceneLoaderForStimulation instance;
    public string sceneToLoad;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void LoadScene()
    {
        TimedCommands.instance.fadePanel.SetActive(true);
        SceneManager.LoadScene(sceneToLoad);

    }
}
