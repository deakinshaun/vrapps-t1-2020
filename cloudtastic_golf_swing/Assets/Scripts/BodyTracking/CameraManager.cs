using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public WebCamTexture webcamTexture;

    public static CameraManager instance;

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
        bool backCam = false;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            UIManager.instance.outputText.text = "No Cameras Detected";           
        }
        else
        {
            UIManager.instance.outputText.text = "No back camera detected";
            if (webcamTexture != null) webcamTexture.Stop();
            webcamTexture = new WebCamTexture(devices[0].name);
            UIManager.instance.rawImage.GetComponent<RawImage>().texture = webcamTexture;
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
    
}
