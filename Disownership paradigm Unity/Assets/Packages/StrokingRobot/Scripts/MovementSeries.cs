﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrokingRobot{
	public class MovementSeries : MonoBehaviour {

		private RobotManagerArvity robotManager;
		public List<int> stepsPerSpeed;
		public List<int> speedPerStep;

		private int currentStep, currentStepRepetition;

        private void Awake()
        {
            robotManager = (RobotManagerArvity)FindObjectOfType<RobotManagerArvity>();
        }
        private void Start() //added
        {
            StartCoroutine(StartMovementTrajectory());
            currentStep = 0;
            currentStepRepetition = 0;
        }

        void Update() {
            /*	if (Input.GetKeyDown("m")) {
                    StartCoroutine(StartMovementTrajectory());
                    currentStep = 0;
                    currentStepRepetition = 0;
                } */ //commented out for study.
        }


        private IEnumerator StartMovementTrajectory(){


			//robotManager.CallDuration ();

			while (!robotManager.IsReadyToStartMovement()) {
				//Debug.Log ("is waiting for robot to come back"); //commented out
				yield return null;
			}

			robotManager.ClearNumberSegment ();
			Debug.Log ("told robot to clear");

			while (!robotManager.acknowledgedInstruction){
				yield return null;
			}

			robotManager.SendMovementSegment (100, speedPerStep [currentStep], 25);//whole lenght

			Debug.Log ("told robot something new movement");

			while (!robotManager.acknowledgedInstruction){
				yield return null;
			}
			
			robotManager.StartMovement ();
			Debug.Log ("told robot to start moving");

			currentStepRepetition++;

			if (currentStepRepetition < stepsPerSpeed [currentStep]) {
				StartCoroutine (StartMovementTrajectory ());
			} 

			else if (currentStepRepetition >= stepsPerSpeed [currentStep]) {
				currentStep++;
				currentStepRepetition = 0;
				StartCoroutine (StartMovementTrajectory ());
			}

			else if (currentStep >= stepsPerSpeed.Count){
				Debug.Log ("there are no more instructions");
			}
				

			yield return null;

		}
	}


}