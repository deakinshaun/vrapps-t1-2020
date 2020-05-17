using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCoreInternal;
using UnityEngine.UI;
using UnityEngine;

public class FaceOutputManager : MonoBehaviour
{
    public bool isTrackingFace = false;
    public float trackingTImer = 0.0f;
    public float smileTimer = 0.0f;
    public bool isSmiling =false;

    public Material colorChangeMat;
    public Color ColorIndicator;

    public Text outputText;
    public Text debugingTime;
    float smileRatio = 0.0f;


   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        
      
        if (isTrackingFace)
        {
             smileRatio = smileTimer / trackingTImer * 100.0f;
             float smileVlaue = Mathf.Clamp(smileRatio, 30, 75)/100;
            ColorIndicator = new Color(Mathf.Clamp((1 - smileVlaue), 0, 1), Mathf.Clamp((smileVlaue), 0, 1), 0, 1.0f);

            debugingTime.text = ("ratio : " + smileVlaue);
            colorChangeMat.color = ColorIndicator;
            trackingTImer = trackingTImer + Time.deltaTime;
            if (isSmiling)
            {
                outputText.text = ("Smiling");
                smileTimer = smileTimer + Time.deltaTime;
            }else if (!isSmiling)
            {
                outputText.text = ("Not Smiling");
                
            }
        }
        else if (!isTrackingFace)
        {
            trackingTImer = 0;
            smileTimer = 0;
        }
    }
}
