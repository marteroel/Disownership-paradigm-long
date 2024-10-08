﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleVAS;
using UnityEngine.SceneManagement;

namespace SimpleVAS 
{
	public class CsvWrite : MonoBehaviour {

		private string condition;
		private static CsvWrite instance = null;
		public static CsvWrite Instance
		{
			get { return instance; }
		}

		//This allows the start function to be called only once.
		void Awake()
		{
			if (instance != null && instance != this) {
				Destroy(this.gameObject);
				return;
			} 
			else 
				instance = this;

			DontDestroyOnLoad(this.gameObject);
		}


		void Start () {
			WriteToFile ("subject ID", "age", "gender", "handedness", "question ID", "questionnaire type", "condition", "delay time", "value");
		}
			

		public void onNextButtonPressed(){
			if (BasicDataConfigurations.ID == null)
				LoadNull ();
			//Changed from package
			//WriteToFile (BasicDataConfigurations.ID, BasicDataConfigurations.age, BasicDataConfigurations.gender, BasicDataConfigurations.handedness, QuestionManager.questionnaireItem,  SceneManager.GetActiveScene().name, ConditionDictionary.selectedOrder[QuestionManager.currentCondition], QuestionManager.VASvalue);
			WriteToFile (BasicDataConfigurations.ID, BasicDataConfigurations.age, BasicDataConfigurations.gender, BasicDataConfigurations.handedness, QuestionManager.questionnaireItem,  
				SceneManager.GetActiveScene().name, ConditionSetter.selectedConditionOrder[QuestionManager.currentCondition], ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition].ToString(), QuestionManager.VASvalue);
		}

		void LoadNull(){
			string na = "na";
			BasicDataConfigurations.ID = na;
			BasicDataConfigurations.age = na;
			BasicDataConfigurations.gender = na;
			BasicDataConfigurations.handedness = na;
			ConditionDictionary.selectedOrder = new string[3] {na, na, na};
		}
		//added an extra row
		void WriteToFile(string a, string b, string c, string d, string e, string f, string g, string h, string i){

			string stringLine =  a + "," + b + "," + c + "," + d + "," + e + "," + f + "," + g + "," + h + "," + i;

			System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + BasicDataConfigurations.ID + "_log.csv", true);
			file.WriteLine(stringLine);
			file.Close();	
		}
	}
}