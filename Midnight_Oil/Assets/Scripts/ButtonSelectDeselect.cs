using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonSelectDeselect : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject Obj;
    public GameObject mat;
    private bool selected = false;

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (selected==false)
        {
            //mat.GetComponent<Renderer>().material.color = new Color(1.1f, .9f, 0.5f);
            Color yellow= new Color(1.1f, .9f, 0.5f);
            mat.GetComponent<Renderer>().material.SetColor("_Color", yellow);
            mat.transform.position += new Vector3(0f, -0.005f, 0f);
            Debug.Log("ButtonSelected");
            selected = true;
        }

        else
        {
            //mat.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
            Color white = new Color(1f,1f,1f);
            mat.GetComponent<Renderer>().material.SetColor("_Color", white);
            mat.transform.position += new Vector3(0f, 0.005f, 0f);
            Debug.Log("Unselected");
            selected = false;
        }

    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
    }
        void Start()
    {

        Obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
}
