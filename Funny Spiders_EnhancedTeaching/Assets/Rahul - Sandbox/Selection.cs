using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject selectedObject;
    public Material selectedMat;
    private Material DefaultMat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter(Collider other)
    {
        selectedObject = other.gameObject;
        DefaultMat = selectedObject.GetComponent<Renderer>().material;
        selectedObject.GetComponent<Renderer>().material = selectedMat;
    }

    private void OnTriggerExit(Collider other)
    {
        Deselect();
    }

    private void Deselect()
    {
        selectedObject.GetComponent<Renderer>().material = DefaultMat;
    }
}
