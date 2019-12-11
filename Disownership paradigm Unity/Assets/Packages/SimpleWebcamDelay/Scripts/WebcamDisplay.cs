using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using SimpleVAS;//added

public class WebcamDisplay : MonoBehaviour {

	private WebCamTexture webcamTexture;
	private Texture2D delayedImage;
	ArrayList myBuffer = new ArrayList();

	public bool setDelay;
	public float delayTimeSeconds;
	public int webcamDeviceID;

	//deleted void Start, now sets the camera from an external script with the SetWebCam function
		
	public void SetWebCam(int webcamIndex) {
		WebCamDevice[] devices = WebCamTexture.devices;

		//webcamIndex = BasicDataConfigurations.selectedWebcamDevice;

		//string deviceName = devices[webcamDeviceID].name;//changed from Unitypackage:
		string deviceName = devices[webcamIndex].name;
		webcamTexture = new WebCamTexture(deviceName, 1024, 768);
		webcamTexture.Play();
	}

	void Update () {
		
		if (webcamTexture.didUpdateThisFrame) {

			if (setDelay) {
				StartCoroutine (ConvertFrame());
				StartCoroutine (DelayWebcam());
			} 

			else if (!setDelay && myBuffer.Count != 0) {
				StopAllCoroutines();
				myBuffer.Clear ();
				Resources.UnloadUnusedAssets ();
			}
		}

	}

	void FixedUpdate () {
		UpdateFrame ();
	}
		

	private void UpdateFrame () {

		if (!setDelay) {
			Renderer renderer = GetComponent<Renderer> ();
			renderer.material.mainTexture = webcamTexture;
		} 

		else if (setDelay) {
			Renderer renderer = GetComponent<Renderer> ();
			renderer.material.mainTexture = delayedImage;
		}

	}

	IEnumerator ConvertFrame(){

		yield return null;

		Texture2D frame = new Texture2D (webcamTexture.width, webcamTexture.height);
		frame.SetPixels32 (webcamTexture.GetPixels32 ());
		myBuffer.Add (frame);

	}

	IEnumerator DelayWebcam(){

		yield return new WaitForFixedTime (delayTimeSeconds); //custom coroutine

		delayedImage = myBuffer [0] as Texture2D;
		delayedImage.Apply();
		myBuffer.RemoveAt (0);
		Resources.UnloadUnusedAssets ();

	}

	void OnDestroy(){
		webcamTexture.Stop ();
	}

		
}
