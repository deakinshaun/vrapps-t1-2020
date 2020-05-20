using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Tiger, Elephant, Star1;

    private bool show = false;

    public void ShowHideInfo()
    {
        if (!show)
        {
            Tiger.SetActive(false);
            Elephant.SetActive(true);
            Star1.SetActive(true);
            show = true;
        }
    }
}
