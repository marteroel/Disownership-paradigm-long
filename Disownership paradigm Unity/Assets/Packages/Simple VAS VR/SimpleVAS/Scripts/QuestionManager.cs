using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;

namespace SimpleVAS
{
	public class QuestionManager : MonoBehaviour {

		List<string> questionList = new List<string>();

		public Text questionUI;
		public Button nextButton;
		public Scrollbar scrollValue;
		public GameObject handle;//added

		public CsvWrite csvWriter;

        //added
        public string stimulationConditionName;

		public bool isForcedChoice;
		public ToggleGroup toggleGroup;

		public static string questionnaireItem, VASvalue;

		private int currentItem;

		public static int currentCondition;

		// Use this for initialization
		void Start () {

			currentItem = 0;
			questionList = CsvRead.questionnaireInput;
			questionUI.text = questionList[currentItem];
			nextButton.interactable = false;
			handle.gameObject.SetActive (false);//added

		}
			
		public void OnSelection(){

			handle.gameObject.SetActive (true);//added
			nextButton.interactable = true;

		}


		public void OnNextButton() {

			nextButton.interactable = false;
			questionnaireItem = currentItem.ToString ();
			if (!isForcedChoice)
				VASvalue = scrollValue.value.ToString ();
			else {
				VASvalue = SelectedToggle.activeToggle.ToString ();
				toggleGroup.SetAllTogglesOff ();
			}
			csvWriter.onNextButtonPressed ();

			currentItem ++;
			handle.gameObject.SetActive (false);//added

			if (currentItem < questionList.Count) 
				questionUI.text = questionList [currentItem];


			else if (currentItem == questionList.Count) {
				currentItem = 0;
				questionList.Clear();


				//Comments for threat study.
				if (isForcedChoice) 
						SceneManager.LoadScene ("VAS");

				else {
					currentCondition = currentCondition + 1;

					//if (currentCondition < ConditionDictionary.selectedOrder.Length)//changed from package to:
					if (currentCondition< ConditionSetter.selectedConditionOrder.Count)
						SceneManager.LoadScene ("Inter");
					//else if (currentCondition == ConditionDictionary.selectedOrder.Length)//changed from package to:
					else if (currentCondition == ConditionSetter.selectedConditionOrder.Count)
						SceneManager.LoadScene ("Goodbye");
				}
			}
		}
	}
}
