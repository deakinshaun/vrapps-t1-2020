using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Vibrate : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject vibrateButton;
    bool ButtonOn = false;

    // Start is called before the first frame update
    void Start()
    {
        vibrateButton = GameObject.Find("VibrationButton");
        vibrateButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (ButtonOn)
        {
            ButtonOn = false;
        }
        else
        {
            ButtonOn = true;
            Handheld.Vibrate();
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Debug.Log("Button Released")
    }
}