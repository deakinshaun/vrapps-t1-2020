using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//a utility class to enable disable the camera
public class CameraManager : MonoBehaviour
{
    public WebCamTexture webcamTexture;

    public static CameraManager instance;
    public bool backCam = false;
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        initCamera();
    }
    public bool initCamera()
    {
        
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            UIManager.instance.outputText.text = "No Cameras Detected";
            backCam = false;
        }
        else
        {
            UIManager.instance.outputText.text = "No back camera detected";
            if (webcamTexture != null) webcamTexture.Stop();
            webcamTexture = new WebCamTexture(devices[0].name);
            UIManager.instance.rawImage.GetComponent<RawImage>().texture = webcamTexture;
            backCam = false;//just using any camera for testing
            webcamTexture.Play();
        }
       
        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                UIManager.instance.outputText.text = "Camera found: " + webcamTexture.deviceName;
                if (webcamTexture != null) webcamTexture.Stop();
                webcamTexture = new WebCamTexture(devices[i].name);
                UIManager.instance.rawImage.GetComponent<RawImage>().texture = webcamTexture;
                backCam = true;
                webcamTexture.Play();
            }
        }
        return backCam;

    }

    public void stopCamera()
    {
        webcamTexture.Stop();
    }
    
}
