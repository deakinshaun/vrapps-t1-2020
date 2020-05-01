using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using UnityEngine.UI;

// Retrieve pose information from the native posenet/tensorflow lite facilities.
// Also includes some visual elements, to show camera feed and monitor retrieved
// values.

public class FetchPose : MonoBehaviour
{


    public Material TrackingDisplayPlaneMaterial;
    private Quaternion baseRotation;
    public GameObject cameraPlane;
    private WebCamTexture webcamTexture;

    private int numPointsInPose = 17;

    public PoseSkeleton poseSkeleton;
    //public NetworkedPose poseTransmitter;

    private bool dataReady;

    private int currentCamera = 0;
    public Text outputText;

    public void Awake()
    {

    }

    private bool overrideCapture = false;

    public void toggleCapture()
    {
        overrideCapture = !overrideCapture;
    }

    private void showCameras()
    {
        outputText.text = "";
        foreach (WebCamDevice d in WebCamTexture.devices)
        {
            outputText.text += d.name + (d.name == webcamTexture.deviceName ? "*" : "") + "\n";
        }
    }

    public void nextCamera()
    {
        currentCamera = (currentCamera + 1) % WebCamTexture.devices.Length;
        // Change camera only works if the camera is stopped.
        webcamTexture.Stop();
        webcamTexture.deviceName = WebCamTexture.devices[currentCamera].name;
        webcamTexture.Play();
        showCameras();
    }

    // Start is called before the first frame update
    void Start()
    {
        webcamTexture = new WebCamTexture();
        showCameras();

        TrackingDisplayPlaneMaterial.mainTexture = webcamTexture;
        //baseRotation = cameraPlane.gameObject.transform.rotation;
        webcamTexture.Play();

        StartCoroutine(prepareModel());
    }


    IEnumerator prepareModel()
    {
        string modelfile = "posenet_mobilenet_v1_100_257x257_multi_kpt_stripped.tflite";
        yield return StartCoroutine(extractFile("", modelfile));

        LoadPlugins.initPose(Application.persistentDataPath + "/" + modelfile);
        //Debug.Log("plugins loaded?");
        dataReady = true;
    }

    // Retrieve the pose from the native library. This returns
    // as an array of floats, containing x, y, and confidence
    // values for each point.
    unsafe float[] retrievePose()
    {
        NativeArray<float> pose = new NativeArray<float>(numPointsInPose * 3, Allocator.Temp);
        int result = LoadPlugins.computePose(webcamTexture.GetNativeTexturePtr(), webcamTexture.width, webcamTexture.height, (float*)NativeArrayUnsafeUtility.GetUnsafePtr(pose));
        //Debug.Log ("Got result " + result + " " + pose[0] + " " + pose[1] + " " + pose[2]);
        return pose.ToArray();
    }

    // Version that retrieves the pose that passes image data
    // directly to the native library, rather that relying on
    // accessing the texture directly. 
    unsafe float[] retrievePoseData()
    {
        Texture2D image = new Texture2D(TrackingDisplayPlaneMaterial.mainTexture.width, TrackingDisplayPlaneMaterial.mainTexture.height, TextureFormat.RGB24, false);
        RenderTexture renderTexture = new RenderTexture(TrackingDisplayPlaneMaterial.mainTexture.width, TrackingDisplayPlaneMaterial.mainTexture.height, 24);
        Graphics.Blit(TrackingDisplayPlaneMaterial.mainTexture, renderTexture);
        RenderTexture.active = renderTexture;

        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        image.Apply();

        NativeArray<float> pose = new NativeArray<float>(numPointsInPose * 3, Allocator.Temp);
        int result = LoadPlugins.computePoseData(image.GetRawTextureData(), image.width, image.height, (float*)NativeArrayUnsafeUtility.GetUnsafePtr(pose));
        // Debug.Log ("Got result " + result + " " + pose[0] + " " + pose[1] + " " + pose[2]);

        Destroy(renderTexture);
        Destroy(image);
        return pose.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        /*   if (  webcamTexture.width < 100)
           {
               Debug.Log("Still waiting another frame for correct info...");
               return;
           }

           // change as user rotates iPhone or Android:

           int cwNeeded = webcamTexture.videoRotationAngle;
           // Unity helpfully returns the _clockwise_ twist needed
           // guess nobody at Unity noticed their product works in counterclockwise:
           int ccwNeeded = -cwNeeded;

           // IF the image needs to be mirrored, it seems that it
           // ALSO needs to be spun. Strange: but true.
           if (webcamTexture.videoVerticallyMirrored) ccwNeeded += 180;

           // you'll be using a UI RawImage, so simply spin the RectTransform
           cameraPlane.localEulerAngles = new Vector3(0f, 0f, ccwNeeded);

           float videoRatio = (float)webcamTexture.width / (float)webcamTexture.height;

           // you'll be using an AspectRatioFitter on the Image, so simply set it
           rawImageARF.aspectRatio = videoRatio;

           // alert, the ONLY way to mirror a RAW image, is, the uvRect.
           // changing the scale is completely broken.
           if (webcamTexture.videoVerticallyMirrored)
               rawImage.uvRect = new Rect(1, 0, -1, 1);  // means flip on vertical axis
           else
               rawImage.uvRect = new Rect(0, 0, 1, 1);  // means no flip

           // devText.text =
           //  videoRotationAngle+"/"+ratio+"/"+wct.videoVerticallyMirrored;
           */





        cameraPlane.gameObject.transform.rotation = baseRotation * Quaternion.AngleAxis(webcamTexture.videoRotationAngle, Vector3.up);



        if (dataReady)// && (overrideCapture || (Input.GetAxis ("Fire1") > 0)))
        {
            //float startTime = Time.realtimeSinceStartup;
            // float [] pose = retrievePose ();
            float[] pose = retrievePoseData();
            // float endTime = Time.realtimeSinceStartup;
            // Debug.Log ("Pose tracked in " + (endTime - startTime).ToString ("F6") + " seconds");

            poseSkeleton.updatePose(pose);
            // poseTransmitter.updatePose (pose);
        }
    }

    // Copy files into an area where they are accessible. This is particularly
    // relevant to packages created for mobile platforms.
    IEnumerator extractFile(string assetPath, string assetFile)
    {
        // Source is the streaming assets path.
        string sourcePath = Application.streamingAssetsPath + "/" + assetPath + assetFile;
        if ((sourcePath.Length > 0) && (sourcePath[0] == '/'))
        {
            sourcePath = "file://" + sourcePath;
        }
        string destinationPath = Application.persistentDataPath + "/" + assetFile;

        // Files must be handled via a WWW to extract.
        WWW w = new WWW(sourcePath);
        yield return w;
        try
        {
            File.WriteAllBytes(destinationPath, w.bytes);
        }
        catch (Exception e)
        {
            Debug.Log("Issue writing " + destinationPath + " " + e);
        }
        Debug.Log(sourcePath + " -> " + destinationPath + " " + w.bytes.Length);
    }

}
