using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject selectedObject;
    private GameObject previousSelection;

    public Material selectedMat;
    public GameObject checkOptionTiger, congratPage, questionScreen;

    public AudioSource source;
    public AudioClip incorrect;

    private Material DefaultMat;
    bool targetAquired = false;
    private bool animalCorrect = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((selectedObject.gameObject.tag == "Tiger") || (selectedObject.gameObject.tag == "Animal"))
        {
            targetAquired = true;
            checkOptionTiger.SetActive(true);
        }
        else
        {
            checkOptionTiger.SetActive(false);
        }
    }

    public void CheckTiger()
    {
        if (selectedObject.gameObject.tag == "Tiger")
        {
            source.Play();
            StartCoroutine(CongratPanel());
        }
        else
        {
            source.clip = incorrect;
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

    IEnumerator CongratPanel()
    {
        yield return new WaitForSeconds(2);
        congratPage.SetActive(true);
        questionScreen.SetActive(false);
    }

    //IEnumerator ThanksPanel()
    //{
    //    yield return new WaitForSeconds(2);
    //    questionScreen.SetActive(true);
    //    congratPage.SetActive(false);
    //}
}
