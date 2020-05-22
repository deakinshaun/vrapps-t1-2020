using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GazeSelectWhale: MonoBehaviour
{

    public GameObject checkOptionAnimal, congratPage, questionScreen;

    private bool animalCorrect = false;

    public Text debug;
    public Material selectedMaterial;
    public Material DefaultMat;
    private GameObject selectedObject;

    public AudioSource correct;
    public AudioSource fail;

    void Start()
    {
        selectedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if ((go.CompareTag("Tiger")) || (go.CompareTag("Elephant")) ||
                (go.CompareTag("Sheep")) || (go.CompareTag("Monkey")))
            {
                debug.text = "FOUND ANIMAL";
                checkOptionAnimal.SetActive(true);
                if (selectedObject == null)
                {
                    selectedObject = go;
                    DefaultMat = selectedObject.GetComponent<Renderer>().material;
                    selectedObject.GetComponent<Renderer>().material = selectedMaterial;
                }
                CheckWrong();
            }
            else if (go.CompareTag("Whale"))
            {
                debug.text = "FOUND ANIMAL";
                checkOptionAnimal.SetActive(true);

                if (selectedObject == null)
                {
                    selectedObject = go;
                    DefaultMat = selectedObject.GetComponent<Renderer>().material;
                    selectedObject.GetComponent<Renderer>().material = selectedMaterial;
                }
                CheckWhale();
            }
        }
        else
        {
            checkOptionAnimal.SetActive(false);
            if (DefaultMat != null)
            {
                selectedObject.GetComponent<Renderer>().material = DefaultMat;
                selectedObject = null;
            }
            debug.text = "NOTHING";
        }
    }

    public void CheckWhale()
    {
        if (Input.GetMouseButtonDown(0))
        {
            correct.Play();
            StartCoroutine(CongratPanel());
        }
    }

    public void CheckWrong()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fail.Play();
        }
    }

    IEnumerator CongratPanel()
    {
        yield return new WaitForSeconds(2);
        congratPage.SetActive(true);
        questionScreen.SetActive(false);
    }
}
