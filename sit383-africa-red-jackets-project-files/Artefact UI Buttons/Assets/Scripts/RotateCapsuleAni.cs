using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RotateCapsuleAni : MonoBehaviour, IVirtualButtonEventHandler
{
    // this script was done as a test to see if button functionality was operational
    public GameObject vbBtnObj;
    public Animator capsuleAni;

    // Start is called before the first frame update
    void Start()
    {
        vbBtnObj = GameObject.Find("TestButton");
        vbBtnObj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        capsuleAni.GetComponent<Animator>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        capsuleAni.Play("capsuleAnimation");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        capsuleAni.Play("none");
    }
}
