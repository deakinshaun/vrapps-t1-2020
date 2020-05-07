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
using System.Diagnostics;

// Retrieve pose information from the native posenet/tensorflow lite facilities.
// Also includes some visual elements, to show camera feed and monitor retrieved
// values.

public class FetchPose : MonoBehaviour
{
    private float poseMultiplier = 1;
    
    private WebCamTexture webcamTexture;

    private int numPointsInPose = 17;

    public PoseSkeleton poseSkeleton;
    

    private bool dataReady;

    public Text outputText;

    private bool isRecording = false;
    private Stopwatch watch;
    private PoseClip currentClip;
    public void toggleCapture()
    {
        isRecording = !isRecording;
        if(isRecording)
        {
            watch.Start();
            currentClip = new PoseClip();
        }
        else
        {
            watch.Stop();
            currentClip.Stopped(watch.ElapsedMilliseconds);
            watch.Reset();
            //TODO - prompt user to keep or discard
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if(devices.Length == 0)
        {
            outputText.text = "No Cameras Detected";
            return;
        }
        for(int i=0;i<devices.Length;i++)
        {
            if(!devices[i].isFrontFacing)
            {
                webcamTexture = new WebCamTexture(devices[i].name);
            }
        }
        if(webcamTexture==null)
        {
            outputText.text = "No back camera detected";
            return;
        }
        else
        {
            outputText.text = "Camera found: " + webcamTexture.deviceName;

            GetComponent<RawImage>().texture = webcamTexture;
            webcamTexture.Play();
            watch = new Stopwatch();
            StartCoroutine(prepareModel());
        }
    }

    private void Ready(bool ready)
    {
        dataReady = ready;
        outputText.text += "\n Dataready:"+dataReady;
    }
    IEnumerator prepareModel()
    {
        string modelfile = "posenet_mobilenet_v1_100_257x257_multi_kpt_stripped.tflite";
        yield return StartCoroutine(extractFile("", modelfile));

        LoadPlugins.initPose(Application.persistentDataPath + "/" + modelfile);
        Ready(true);


    }

    // Retrieve the pose from the native library. This returns
    // as an array of floats, containing x, y, and confidence
    // values for each point.
    unsafe float[] retrievePose()
    {
        NativeArray<float> pose = new NativeArray<float>(numPointsInPose * 3, Allocator.Temp);
        int result = LoadPlugins.computePose(webcamTexture.GetNativeTexturePtr(), webcamTexture.width, webcamTexture.height, (float*)NativeArrayUnsafeUtility.GetUnsafePtr(pose));
        return pose.ToArray();
    }

    // Version that retrieves the pose that passes image data
    // directly to the native library, rather that relying on
    // accessing the texture directly. 
    unsafe float[] retrievePoseData()
    { 
        Texture2D image = new Texture2D(GetComponent<RawImage>().mainTexture.width, GetComponent<RawImage>().mainTexture.height, TextureFormat.RGB24, false);
        RenderTexture renderTexture = new RenderTexture(GetComponent<RawImage>().mainTexture.width, GetComponent<RawImage>().mainTexture.height, 24);
        Graphics.Blit(GetComponent<RawImage>().mainTexture, renderTexture);
        RenderTexture.active = renderTexture;

        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        image.Apply();

        NativeArray<float> pose = new NativeArray<float>(numPointsInPose * 3, Allocator.Temp);
        LoadPlugins.computePoseData(image.GetRawTextureData(), image.width, image.height, (float*)NativeArrayUnsafeUtility.GetUnsafePtr(pose));
        
        Destroy(renderTexture);
        Destroy(image);
        return pose.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
       
        poseMultiplier = GetComponent<RawImage>().mainTexture.height/40f;

        if (dataReady && isRecording )
        {
            float[] pose = retrievePoseData();
            
            if(pose.Length > 0)
            {
                poseSkeleton.updatePose(pose, poseMultiplier);
                //record frame
                watch.Stop();
                currentClip.addFrame(new PoseData(pose,watch.ElapsedMilliseconds));
                watch.Start();
            }
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
            UnityEngine.Debug.Log("Issue writing " + destinationPath + " " + e);
        }
        UnityEngine.Debug.Log(sourcePath + " -> " + destinationPath + " " + w.bytes.Length);
    }

}
