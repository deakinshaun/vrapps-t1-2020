using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Pun;

public class SpeakerIndicator : MonoBehaviour
{
    private Recorder recorder;
    public GameObject speakerObject;

    // Start is called before the first frame update
    void Start()
    {
        recorder = GetComponent<Recorder>();    
    }

    // Update is called once per frame
    void Update()
    {
        speakerObject.transform.localScale = Vector3.one * ((recorder.IsCurrentlyTransmitting) ? 1 : 0);
    }
}
