using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RadioSelection : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject Obj;
    public GameObject Radio1Mesh;
    public GameObject Radio2Mesh;
    public Material Selected;
    public Material Unselected;

    private bool Radio1 = false;

    Color yellow = new Color(1.1f, .9f, 0.5f);
    Color white = new Color(1f, 1f, 1f);

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {

            Radio1Mesh.GetComponent<Renderer>().material = Selected;
            Radio1Mesh.GetComponent<Renderer>().material.SetColor("_Color", yellow);
            Radio1Mesh.transform.position += new Vector3(0f, -0.005f, 0f);
                
            Radio2Mesh.GetComponent<Renderer>().material = Unselected;
            Radio1 = true;
        


    }


    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        if (Radio1 == true)
        {

            Radio1Mesh.GetComponent<Renderer>().material.SetColor("_Color", white);
            Radio1Mesh.transform.position += new Vector3(0f, 0.005f, 0f);
            Radio1 = false;
        }

    }


    void Start()
    {
        Obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

}
