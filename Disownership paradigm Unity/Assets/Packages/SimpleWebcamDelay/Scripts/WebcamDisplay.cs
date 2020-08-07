using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using SimpleVAS;//added

public class WebcamDisplay : MonoBehaviour {

	private WebCamTexture webcamTexture;

	public bool setDelay;
	public float delayTimeSeconds;
	public int webcamDeviceID;


    private int buffer_index = 0;
    private int buffer_size = 60;
    private int buffer_current = 0;

    private RenderTexture[] renderBuffer;
    private long[] timeBuffer;
    private Texture _delayedFeed;

    private Renderer _renderer;


    private void Awake()
    {
         _renderer = GetComponent<Renderer>();
    }
    public void SetWebCam(int webcamIndex) {
		WebCamDevice[] devices = WebCamTexture.devices;

		string deviceName = devices[webcamIndex].name;
		webcamTexture = new WebCamTexture(deviceName, 1024, 768, 30);
		webcamTexture.Play();

        _delayedFeed = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, false);

        InitializeBuffer();
	}

    void InitializeBuffer()
    {
        renderBuffer = new RenderTexture[buffer_size];
        for(int ii = 0; ii < buffer_size; ii++)
            renderBuffer[ii] = new RenderTexture(webcamTexture.width, webcamTexture.height, 24, RenderTextureFormat.ARGB32);
        timeBuffer = new long[buffer_size];
    }

    private void Update()
    {
        if (setDelay)
            if (webcamTexture.didUpdateThisFrame)
                Delay(webcamTexture);

        UpdateFrame();
    }

    void Delay(Texture _source)
    {
        long time = System.DateTime.Now.Ticks;
        Graphics.Blit(_source, renderBuffer[buffer_index]);
        timeBuffer[buffer_index] = time;

        long delayTime = time - (long)(delayTimeSeconds * 10000000);

        // Binary Search
        int from = 0, to = buffer_size, mid;
        while (from != to)
        {
            mid = (from + to) / 2;
            if (timeBuffer[(mid + buffer_index + 1) % buffer_size] > delayTime)
                to = mid;
            else
                from = mid + 1;
        }

        buffer_current = (from + buffer_index) % buffer_size;

        Graphics.CopyTexture(renderBuffer[buffer_current], _delayedFeed);

        buffer_index = (buffer_index + 1) % buffer_size;
    }
		

	private void UpdateFrame () {

		if (!setDelay) {
			_renderer.material.mainTexture = webcamTexture;
		} 

		else if (setDelay) {
			_renderer.material.mainTexture = _delayedFeed;
		}

	}


	void OnDestroy(){
		webcamTexture.Stop ();
	}

		
}
