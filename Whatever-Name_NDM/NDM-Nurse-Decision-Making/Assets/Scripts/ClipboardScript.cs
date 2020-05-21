using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardScript : MonoBehaviour
{
    public Text ClipboardTextbox;
    public string PatientName;
    public string PatientGender;
    public string PatientAge;
    public string PatientAllergies;
    public string PatientHealthConditions;

    public GameObject Clipboard;
    private bool PopupEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        ClipboardTextbox.text = "Patient Information" + "\r\n" +
                                "Name: " + PatientName + "\r\n" +
                                "Gender: " + PatientGender + "\r\n" +
                                "Age: " + PatientAge + "\r\n" +
                                "Allergies: " + PatientAllergies + "\r\n" +
                                "Health Conditions: " + PatientHealthConditions + "\r\n";

    }

    private void PopupTrue ()
    {
        PopupEnabled = true;
        Debug.Log("Popup Enabled =" + PopupEnabled);
    }
    private void PopupFalse ()
    {
    PopupEnabled = false;
    Debug.Log("Popup Enabled =" + PopupEnabled);
    }

    public void Popup ()
    {
        if (PopupEnabled == false)
        {
            Invoke("PopupTrue", 0.1f);
        }

        if (PopupEnabled == true)
        {
            Invoke("PopupFalse", 0.1f);
        }
    }

    private void Update()
    {
        if (PopupEnabled == true)
        {
            if (transform.position.y < -1.0f)
            {
                transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            }
        }

        if (PopupEnabled == false)
        {
            if (transform.position.y > -1.55f)
            {
                transform.Translate(Vector3.down * Time.deltaTime, Space.World);
            }
        }
    }
}
