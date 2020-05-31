using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ModelSwap : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject Obj;
    public GameObject[] objects;
    int i = 0;

    public void SetObjects()
    {
        objects[0].SetActive(true);
    }
    
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        objects[i].SetActive(false);
        i++;

        if (i >= objects.Length)
            i = 0;

        objects[i].SetActive(true);
    }
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
    }
    void Start()
    {
        SetObjects();

        Obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
}
