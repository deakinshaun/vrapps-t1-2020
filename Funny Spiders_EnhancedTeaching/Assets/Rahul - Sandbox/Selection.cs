using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject selectedObject;
    public Material selectedMat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        selectedObject = other.gameObject;
        selectedObject.GetComponent<Renderer>().material = selectedMat;
    }
}
