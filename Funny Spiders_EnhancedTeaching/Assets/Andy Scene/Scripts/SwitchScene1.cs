using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene1 : MonoBehaviour
{
    public GameObject congratScene, newAnimalQuestion, star;
    // Start is called before the first frame update
    public void MoveOnScene()
    {
        StartCoroutine(LoadNextPanel());
    }

    IEnumerator LoadNextPanel()
    {
        yield return new WaitForSeconds(1);
        congratScene.SetActive(false);
        newAnimalQuestion.SetActive(true);
        star.SetActive(true);
    }
}
