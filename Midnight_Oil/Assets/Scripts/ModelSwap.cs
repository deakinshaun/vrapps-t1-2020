using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ModelSwap : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject Obj;
    public GameObject[] objects;
    int i = 0;

    public void HideObjects()
    {
        objects[0].SetActive(true);
        objects[1].SetActive(false);
        objects[2].SetActive(false);
        objects[3].SetActive(false);
        objects[4].SetActive(false);
        objects[5].SetActive(false);
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
        HideObjects();

        Obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
}
