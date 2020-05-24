using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class SndDisplayHistory : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject virtualSndButton;
    public TextMesh sndHistoryLog;
    bool ButtonOn = false;

    // Start is called before the first frame update
    void Start()
    {
        virtualSndButton = GameObject.Find("SoundHistButton");
        virtualSndButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        TextMesh sndHistoryLog = GameObject.Find("SndHistoryLog").GetComponent<TextMesh>();
        sndHistoryLog.gameObject.SetActive(false);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (ButtonOn)
        {
            ButtonOn = false;
            sndHistoryLog.gameObject.SetActive(false);
        }
        else
        {
            ButtonOn = true;
            sndHistoryLog.gameObject.SetActive(true);
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Debug.Log("Button Released");
    }

}
