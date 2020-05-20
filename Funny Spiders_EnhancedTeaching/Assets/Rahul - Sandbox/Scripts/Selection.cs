using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject selectedObject;
    private GameObject currentSelection;
    private GameObject previousSelection;
    public Material selectedMat;
    public AnimalMovement animalMovementScript;
    private Material DefaultMat;
    bool targetAquired;
    public Text debugtext;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((previousSelection.gameObject.tag == "Animal")&&(selectedObject.gameObject.tag == "Habitat"))
        {
            debugtext.text = "I HAVE A TARGET";
            targetAquired = true;

            animalMovementScript = previousSelection.GetComponent<AnimalMovement>();
            if (!animalMovementScript.hasTarget)
            {
                animalMovementScript.setTarget(selectedObject);
            }

        }
        else
        {
            debugtext.text = "Just another habitat";
        }
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
   
        previousSelection = selectedObject;
        
       
        selectedObject.GetComponent<Renderer>().material = DefaultMat;
    }
}
