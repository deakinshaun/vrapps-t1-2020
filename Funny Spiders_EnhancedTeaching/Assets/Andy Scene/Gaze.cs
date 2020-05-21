using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Gaze: MonoBehaviour
{

    public GameObject checkOptionAnimal, congratPage, questionScreen;

    private bool animalCorrect = false;

    public Text debug;

    public AudioSource source;
    public AudioClip incorrect;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if ((go.CompareTag("Tiger")) || (go.CompareTag("Elephant")) || (go.CompareTag("Monkey")) ||
                (go.CompareTag("Sheep")) || (go.CompareTag("Whale")))
            {
                debug.text = "FOUND IT";
                checkOptionAnimal.SetActive(true);
            }

            if (go.CompareTag("Tiger"))
            {
                debug.text = "FOUND TIGER";
                CheckTiger();
            }
        }
        else
        {
            debug.text = "NOTHING";
            checkOptionAnimal.SetActive(false);
        }
    }

    public void CheckTiger()
    {
        if (Input.GetMouseButtonDown(0))
        {
            source.Play();
            StartCoroutine(CongratPanel());
        }
        else
        {
            source.clip = incorrect;
        }
    }

    IEnumerator CongratPanel()
    {
        yield return new WaitForSeconds(2);
        congratPage.SetActive(true);
        questionScreen.SetActive(false);
    }
}
