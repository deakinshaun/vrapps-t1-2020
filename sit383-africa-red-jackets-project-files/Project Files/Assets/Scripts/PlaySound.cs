using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class PlaySound : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject soundButton;
    private AudioSource audioSource;
    bool ButtonOn = false;

    // Start is called before the first frame update
    void Start()
    {
        soundButton = GameObject.Find("SoundButton");
        soundButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.enabled = false;
        audioSource.loop = true;
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (ButtonOn)
        {
            ButtonOn = false;
            audioSource.Stop();
            audioSource.enabled = false;
            //audioSource.mute = !audioSource.mute;
        }
        else
        {
            ButtonOn = true;
            audioSource.enabled = true;
            audioSource.Play();
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Debug.Log("Button Released")
    }
}
