using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArtefactInteraction : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    public float upDown = 0.0f;
    public float leftRight = 0.0f;
    public Slider sizeSlider;
    public Text artefactSize;

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Canvas/ArtefactInteraction/LeftButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { rotateLeft(); });
        GameObject.Find("Canvas/ArtefactInteraction/LeftButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });

        GameObject.Find("Canvas/ArtefactInteraction/RightButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { rotateRight(); });
        GameObject.Find("Canvas/ArtefactInteraction/RightButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });

        GameObject.Find("Canvas/ArtefactInteraction/UpButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { rotateUp(); });
        GameObject.Find("Canvas/ArtefactInteraction/UpButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });

        GameObject.Find("Canvas/ArtefactInteraction/DownButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { rotateDown(); });
        GameObject.Find("Canvas/ArtefactInteraction/DownButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });

        this.transform.rotation *= Quaternion.AngleAxis(rotateSpeed, new Vector3(upDown, 0f, leftRight));

        this.transform.localScale = new Vector3(sizeSlider.value, sizeSlider.value, sizeSlider.value);
        artefactSize.text = sizeSlider.value.ToString("F1");
    }
    
    public void rotateUp()
    {
        upDown = 1.0f;
    }

    public void rotateDown()
    {
        upDown = -1.0f;
    }

    public void rotateLeft()
    {
        leftRight = 1.0f;
    }

    public void rotateRight()
    {
        leftRight = -1.0f;
    }

    public void stop()
    {
        upDown = 0.0f;
        leftRight = 0.0f;
    }
}
