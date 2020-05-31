using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Voice.Unity;
using Photon.Pun;

public class RecordController : MonoBehaviour
{
    public bool isRecording = false;
    public bool isPlayback = false;
    private RecordableObject[] playbackObjects;
    private int currentFrame;
    private int frameCount;
    public GameObject timeline;
    public Image timelineImage;
    public TextMeshProUGUI text;
    public Sprite micBroadcast;
    public Sprite micMute;
    public Image micImage;
    private bool micOn = true;

    // a float that relates to the integer of the current frame
    // for the thumbstick to scroll through playback mode
    private float sliderState;


    // Start is called before the first frame update
    void Start()
    {
        timeline.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            micOn = !micOn;
            if (micOn)
            {
                micImage.sprite = micBroadcast;
            }
            else
            {
                micImage.sprite = micMute;
            }

            Recorder[] recorders = FindObjectsOfType<Recorder>();
            foreach (Recorder recorder in recorders)
            {
                if (recorder.GetComponent<PhotonView>().IsMine)
                {
                    recorder.TransmitEnabled = micOn;
                }
            }
        }
        
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            // get a list of recordable objects for playback
            isPlayback = !isPlayback;
            if (isPlayback)
            {
                playbackObjects = GameObject.FindObjectsOfType<RecordableObject>();
                if (playbackObjects.Length == 0)
                {
                    isPlayback = false;
                    return;
                }
                
                timeline.SetActive(true);
                frameCount = playbackObjects[0].record.Count - 1;
                currentFrame = 0;
                Debug.Log("Found " + playbackObjects.Length + " objects with recorded data.");
                Debug.Log("Framecount: " + frameCount);
                text.text = "Playback Mode";
                sliderState = 0f;
            }
            else
            {
                // getting out of playback mode
                // go into playback mode again or go into record mode
                foreach (RecordableObject recordable in playbackObjects)
                {
                    recordable.Reset();
                }

                timeline.SetActive(false);
                text.text = "";
            }
        }

        if (isPlayback)
        {
            // Debug.Log(currentFrame + "/" + frameCount);
            float x = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x;
            
            // allow a bit of a deadzone
            if (Mathf.Abs(x) > 0.05f)
            {
                sliderState += (x * Time.unscaledDeltaTime);
                sliderState = Mathf.Clamp(sliderState, 0, frameCount);
                currentFrame = Mathf.RoundToInt((float)frameCount * sliderState);
                Debug.Log(currentFrame);

                foreach (RecordableObject recordable in playbackObjects)
                {
                    recordable.SetTransform(currentFrame);
                }

                timelineImage.fillAmount = (float)currentFrame / (float)frameCount;
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                isRecording = !isRecording;
                if (isRecording)
                {
                    text.text = "Recording Mode";
                    StartRecording();
                }
                else
                {
                    StopRecording();
                    text.text = "";
                }
            }
        }
    }

    public void StartRecording()
    {
        RecordableObject[] recordables = GameObject.FindObjectsOfType<RecordableObject>();
        if (recordables.Length == 0)
        {
            isRecording = false;
            return;
        }        
        
        foreach (RecordableObject recordable in recordables)
        {
            recordable.Record();
        }
    }

    public void StopRecording()
    {
        RecordableObject[] recordables = GameObject.FindObjectsOfType<RecordableObject>();
        foreach (RecordableObject recordable in recordables)
        {
            recordable.Stop();
            recordable.Reset();
        }
    }
}
