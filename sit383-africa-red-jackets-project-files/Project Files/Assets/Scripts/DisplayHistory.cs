using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class DisplayHistory : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject virtualVibButton;
    public TextMesh vibHistoryLog;
    bool ButtonOn = false;
   
    // Start is called before the first frame update
    void Start()
    {
        virtualVibButton = GameObject.Find("VibrateHistButton");
        virtualVibButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        TextMesh vibHistoryLog = GameObject.Find("VibHistoryLog").GetComponent<TextMesh>();
        vibHistoryLog.gameObject.SetActive(false);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (ButtonOn)
        {
            ButtonOn = false;
            vibHistoryLog.gameObject.SetActive(false);
        }
        else
        {
            ButtonOn = true;
            vibHistoryLog.gameObject.SetActive(true);
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Debug.Log("Button Released");
    }

}
