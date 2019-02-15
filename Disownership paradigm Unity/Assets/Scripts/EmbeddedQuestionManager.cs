using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;

public class EmbeddedQuestionManager : MonoBehaviour
{

    List<string> questionList = new List<string>();

    public Text questionUI;
    public Button nextButton;
    public Scrollbar scrollValue;
    public GameObject handle;//added
    public CsvWrite csvWriter;
    public int numberOfRepetitions;
    public bool useSceneManually;

    public GameObject uiObject;
    public GameObject guiReticle;

    private int currentItem;

    private int currentRepetition;

    // Use this for initialization
    void Start()
    {
        currentItem = 0;
        questionList = CsvRead.questionnaireInput;
        questionUI.text = questionList[currentItem];
        nextButton.interactable = false;
        handle.gameObject.SetActive(false);//added
        uiObject.SetActive(false);
        guiReticle.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            uiObject.SetActive(true);
            guiReticle.SetActive(true);
        }
    }

    public void OnSelection()
    {
        handle.gameObject.SetActive(true);//added
        nextButton.interactable = true;
    }


    public void OnNextButton() {

        nextButton.interactable = false;
        
        QuestionManager.questionnaireItem = currentItem.ToString();
        QuestionManager.VASvalue = scrollValue.value.ToString();


        if(!useSceneManually)
            csvWriter.onNextButtonPressed();
        
         currentItem++;
         handle.gameObject.SetActive(false);
    
        if (currentItem < questionList.Count) { 
            questionUI.text = questionList[currentItem];
        }

        else if (currentItem == questionList.Count) {
          currentItem = 0;
           
            currentRepetition++;
            

            if (currentRepetition < numberOfRepetitions) 
             questionUI.text = questionList[currentItem];

            else if (currentRepetition == numberOfRepetitions) {
                if (!useSceneManually) {
                    if (!BasicDataConfigurations.finishOnduration)
                    {
                        SceneLoaderForStimulation.instance.LoadScene();
                        questionList.Clear();
                    }
                    else questionUI.text = "no more content to show";
                }

                else {
                    Debug.Log("done testing");
                    questionUI.text = "no more content to show";
                }
                
            }
            StartCoroutine(TurnOffUI());
        }
    }

    private IEnumerator TurnOffUI()
    {
        yield return null;
       // yield return new WaitForSeconds(1f);
        uiObject.SetActive(false);
        guiReticle.SetActive(false);
    }
}
