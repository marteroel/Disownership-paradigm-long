using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleVAS;//added

namespace StrokingRobot{
	public class MovementSeries : MonoBehaviour {

		private RobotManagerArvity robotManager;

        public bool sendMessagesToServer;

		public List<int> stepsPerSpeed;
		public List<int> speedPerStep;

		private int currentStep, currentStepRepetition;
        private TCPClient tcpCommunicator;

        private void Awake()
        {
            robotManager = (RobotManagerArvity)FindObjectOfType<RobotManagerArvity>();
            tcpCommunicator = FindObjectOfType<TCPClient>();
        }
        private void Start() //added
        {
            StartCoroutine(WaitForRobotToBeReady());
        }

        void Update() {
            /*	if (Input.GetKeyDown("m")) {
                    StartCoroutine(StartMovementTrajectory());
                    currentStep = 0;
                    currentStepRepetition = 0;
                } */ //commented out for study.
        }

        //added
        private IEnumerator WaitForRobotToBeReady()
        {
            while (!robotManager.IsReadyToStartMovement()) {
                yield return null;
                //Debug.Log("waiting for him to arrive");
            }


            StartCoroutine(StartMovementTrajectory());
            currentStep = 0;
            currentStepRepetition = 0;

            TimedCommands.instance.StartLoadSceneCoroutine();//Uncomment for experiment

            if(sendMessagesToServer)
                tcpCommunicator.SendTCPMessage("start block " + QuestionManager.currentCondition.ToString() + " delay " + ConditionSetter.selectedDelayOrder[QuestionManager.currentCondition].ToString());
        }


        private IEnumerator StartMovementTrajectory(){

			//robotManager.CallDuration ();

			while (!robotManager.IsReadyToStartMovement()) {
				//Debug.Log ("is waiting for robot to come back"); //commented out
				yield return null;
			}

			robotManager.ClearNumberSegment ();
			//Debug.Log ("told robot to clear");

			while (!robotManager.acknowledgedInstruction){
				yield return null;
			}
            Debug.Log("SEND MOVEMENT SEGMENT from coroutine");
			robotManager.SendMovementSegment (100, speedPerStep [currentStep], robotManager.forwardAngle, robotManager.returnAngle);//whole lenght


           // Debug.Log ("told robot something new movement");
            while (!robotManager.acknowledgedInstruction){
				yield return null;
			}

            Debug.Log("START MOVEMENT from coroutine and is ready is " + robotManager.IsReadyToStartMovement());
			robotManager.StartMovement ();

            if (sendMessagesToServer)
                tcpCommunicator.SendTCPMessage("begin trial at speed: " + speedPerStep[currentStep].ToString());
            //Debug.Log ("told robot to start moving");

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