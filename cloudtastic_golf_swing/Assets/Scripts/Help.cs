using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject helpCanvas;
    private bool helpEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HelpToggle()
    {
        if(helpEnabled)
        {            
            helpCanvas.SetActive(false);
            helpEnabled = false;
        }
        else
        {            
            helpCanvas.SetActive(true);
            helpEnabled = true;
        }

    }
}
