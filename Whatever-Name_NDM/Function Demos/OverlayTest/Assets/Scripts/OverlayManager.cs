using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    public bool OverlayShown = false;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OverlayShown == true)
        {
            Canvas.SetActive(true);
        }
        if(OverlayShown == false)
        {
            Canvas.SetActive(false);
        }
    }


}
