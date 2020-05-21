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

    public AudioSource correct;
    public AudioSource fail;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if ((go.CompareTag("Tiger")) || (go.CompareTag("Elephant")) ||
                (go.CompareTag("Sheep")) || (go.CompareTag("Monkey")))
            {
                debug.text = "FOUND IT";
                checkOptionAnimal.SetActive(true);
            }
            else if (go.CompareTag("Whale"))
            {
                CheckWhale();
                debug.text = "FOUND WHALE";
            }
        }
        else
        {
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
