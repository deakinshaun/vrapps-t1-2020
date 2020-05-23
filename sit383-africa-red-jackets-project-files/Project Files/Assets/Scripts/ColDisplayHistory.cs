using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ColDisplayHistory : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject virtualColButton;
    public TextMesh colHistoryLog;
    bool ButtonOn = false;

    // Start is called before the first frame update
    void Start()
    {
        virtualColButton = GameObject.Find("ColourHistButton");
        virtualColButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        TextMesh colHistoryLog = GameObject.Find("ColHistoryLog").GetComponent<TextMesh>();
        colHistoryLog.gameObject.SetActive(false);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (ButtonOn)
        {
            ButtonOn = false;
            colHistoryLog.gameObject.SetActive(false);
        }
        else
        {
            ButtonOn = true;
            colHistoryLog.gameObject.SetActive(true);
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Debug.Log("Button Released");
    }

}
