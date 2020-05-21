using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject animal, Box, suggestion;

    Vector3 AnimalIni;

    public GameObject AnimalQuestion, congratPage;

    private bool animalCorrect = false;

    int numWrong = 0;

    Vector3 iniScaleAnimal;

    public AudioSource source;

    void Start()
    {
        AnimalIni = animal.transform.position;

        iniScaleAnimal = animal.transform.localScale;
    }

    public void DragAnimal()
    {
        animal.transform.position = Input.mousePosition;
    }

    public void DropAnimal()
    {
        float Distance = Vector3.Distance(animal.transform.position, Box.transform.position);


        if (Distance < 5 && animalCorrect==false)
        {
            animal.transform.localScale = Box.transform.localScale;
            animal.transform.position = Box.transform.position;
            animalCorrect = true;
            Box.name = animal.name;
            source.Play();
            StartCoroutine(CongratPanel());
        }
        else
        {
            animal.transform.position = AnimalIni;
            numWrong++;
            if (numWrong >= 5)
            {
                suggestion.SetActive(true);
            }
        }
    }

    //public void ShowHideInfo()
    //{
    //    if (!show)
    //    {
    //        Animal.SetActive(false);
    //        Elephant.SetActive(true);
    //        Star1.SetActive(true);
    //        show = true;
    //        StartCoroutine(LoadNextPanel());
    //    }
    //}

    IEnumerator CongratPanel()
    {
        yield return new WaitForSeconds(2);
        congratPage.SetActive(true);
        AnimalQuestion.SetActive(false);
    }

}
