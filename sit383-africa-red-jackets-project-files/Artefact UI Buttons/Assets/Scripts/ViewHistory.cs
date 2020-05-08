using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ViewHistory : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject virtualButton;
    // public TextMesh historyTitle; This has simply been incorpated into historyLog now
    public TextMesh historyLog;
    // public Animator hisLogAnimation; Though the animation appraoch wasn't undertaken in the end this is a good reference as to how it is done if required in the future
    // public Animator hisLogREAnimation;
    bool ButtonOn = false;
    // private Vector3 hiddenPosition; This whole process involved trying to move the text itself to hide behind the plane so if would technically 'dissapear' from view but would actually be hidden behind another object, a custom shader was developed in order to have 3D Text dissapear in such a fashion.
    // private Vector3 revealedPosition; The shader constructed will also be kept within this program as a reference for any possible future use.
    

    // Start is called before the first frame update
    void Start()
    {
        virtualButton = GameObject.Find("HistoryButton");
        // historyTitle = GameObject.Find("HistoryTitle").GetComponent<TextMesh>();
        // historyLog = GameObject.Find("HistoryLog").GetComponent<TextMesh>();
        virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        // None of these methods work due to 3D Text's restrictions, it's far too different from UI Text to be manipulated in a similar manner
        // historyTitle.GetComponent<TextMesh>.enabled = false;
        // historyLog.GetComponent<TextMesh>.enabled = false;

        // historyTitle.GetComponent<TextMesh>.enabled = false;
        // historyLog.GetComponent<TextMesh>.enabled = false;

        // historyTitle.GetComponent(typeof(Text).enabled = false;
        // historyLog.GetComponent(typeof(Text).enabled = false;

        // historyTitle.enabled = false;
        // historyLog.enabled = false;

        // hisLogAnimation.GetComponent<Animator>(); // taking an animation approach to hide the text away untill the button is pressed
        // hisLogREAnimation.GetComponent<Animator>();

        //hiddenPosition = transform.position;
        //revealedPosition = new Vector3(0.539f, 0.133f, 0.353f);
        // historyLog.transform.position = new Vector3(0.488f, -1.0f, 0.321f); // can refine values alter, purpose is to just make text be elsewhere for now
        TextMesh historyLog = GameObject.Find("HistoryLog").GetComponent<TextMesh>();
        //historyLog.enable = false;
        historyLog.gameObject.SetActive(false);
    }

    public void OnButtonPressed (VirtualButtonBehaviour vb)
    {
        if (ButtonOn)
        {
            ButtonOn = false;
            // historyTitle.GetComponent(Text).enabled = true; 3D Text doesn't have the ability to be enabled or not
            // historyLog.GetComponent(Text).enabled = true;

            // historyTitle.CrossFadeAlpha(1.0f, 0.05f, false); 3D Text can't fade due to not containing properties of text function
            // historyLog.CrossFadeAlpha(1.0f, 0.05f, false);

            // historyLog.use.Alpha(GetComponent(TextMesh).renderer.material, 0.0, 1.0, 2.0); // 3D Text isn't valid within the context, making it unable to use the CrossFadeAlpha

            // hisLogAnimation.Play("historyLog_Animation"); // created an annimation of the text fading in, however, this would cause the test to only appear for the duration of the animation
            // GameObject.Find("HistoryLog").GetComponent<TextMesh>().color = Color.white; // tried to make text blend in with the plane beneath it so it would be 'hidden'

            // historyLog.transform.position = new Vector3(-1f, -2.0f, 0.321f); // can refine values alter, purpose is to just make text be elsewhere for now

            // Vector3 pos = historyLog.transform.position;
            // pos.X += 0.133;
            // historyLog.transform.position = pos;

            //historyLog.enable = false;
            historyLog.gameObject.SetActive(false);
        }
        else
        {
            ButtonOn = true;
            // historyTitle.GetComponent(Text).enabled = false; same as above
            // historyLog.GetComponent(Text).enabled = false;

            // historyTitle.CrossFadeAlpha(0.0f, 0.05f, false); same as above
            // historyLog.CrossFadeAlpha(0.0f, 0.05f, false);

            // hisLogREAnimation.Play("historyLogRE_Animation");

            // hisLogAnimation.Play("none");

            //GameObject.Find("HistoryLog").GetComponent<TextMesh>().color = Color.black;

            // historyLog.transform.position = new Vector3(0.488f, 0.133f, 0.321f); // move 3D Text y position to be infront of plane, rather than behind it

            //historyLog.enable = true;
            historyLog.gameObject.SetActive(true);
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // Debug.Log("Button Released");
    }

}
