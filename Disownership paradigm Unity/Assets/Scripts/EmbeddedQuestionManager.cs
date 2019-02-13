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

        public static string questionnaireItem, VASvalue;

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

        }

        public void OnSelection()
        {
            handle.gameObject.SetActive(true);//added
            Debug.Log("selected");
            nextButton.gameObject.SetActive(true);
            nextButton.interactable = true;   
        }


    public void OnNextButton() {

        Debug.Log("next button turned");
        nextButton.interactable = false;
        nextButton.gameObject.SetActive(false);
        questionnaireItem = currentItem.ToString();

        VASvalue = scrollValue.value.ToString();

        csvWriter.onNextButtonPressed();

        currentItem++;
        handle.gameObject.SetActive(false);//added

        if (currentItem < questionList.Count) { 
        questionUI.text = questionList[currentItem];
        Debug.Log("this should be the case, with item of the questionnaire no: " + currentItem);
     }

            else if (currentItem == questionList.Count)
            {
                currentItem = 0;
                questionList.Clear();

                currentRepetition++;

            if (currentRepetition < numberOfRepetitions)
            {
                questionList = CsvRead.questionnaireInput;
                Debug.Log("reading the next item");
            }
            if (currentRepetition == numberOfRepetitions) {
                if (!BasicDataConfigurations.finishOnduration)
                    SceneLoaderForStimulation.instance.LoadScene("VAS");
                else questionUI.text = "no more content to show"; 
                }
            }
        }
    }
