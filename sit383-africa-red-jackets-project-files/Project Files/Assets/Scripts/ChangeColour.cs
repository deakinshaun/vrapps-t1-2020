using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ChangeColour : MonoBehaviour, IVirtualButtonEventHandler
{
    //Renderer rend;
    public GameObject colourButton;
    public GameObject colourCube;
    Renderer cubeRenderer;
    bool ButtonOn = false;

    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<Renderer>();
        colourCube = GameObject.Find("Cube");
        //rend = GetComponent<Renderer>();
        cubeRenderer = colourCube.GetComponent<Renderer>();
        colourButton = GameObject.Find("ColourButton");
        colourButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
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
            cubeRenderer.material.SetColor("_Color", Random.ColorHSV());
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Debug.Log("Button Released")
    }

}
