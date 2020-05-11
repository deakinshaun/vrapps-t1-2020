using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class TestButton : MonoBehaviour, IVirtualButtonEventHandler
{ 
    public GameObject Obj;
    public GameObject mat;

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        mat.GetComponent<Renderer>().material.color= new Color (1f,1f,0f);
        mat.transform.position += new Vector3(0f, -0.005f, 0f);
        Debug.Log("ButtonPressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        mat.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
        mat.transform.position += new Vector3(0f, 0.005f, 0f);
        Debug.Log("ButtonReleased");
    } 

    void Start ()
    {
        Obj = GameObject.Find("Button");

        Obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        

    }
}
