using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardText : MonoBehaviour
{
    public string PatientName;
    public string Gender;
    public string PatientAge;
    public string Allergies;
    public string PatientHealthConditions;

    public Text ClipboardTextbox;

    // Start is called before the first frame update
    void Start()
    {
        ClipboardTextbox.GetComponent<Text>();

        ClipboardTextbox.text = "Name: " + PatientName + "\r\n" +
                                "Gender: " + Gender + "\r\n" +
                                "Age: " + PatientAge + "\r\n" +
                                "Allergies: " + Gender + "\r\n" +
                                "Health Conditions: " + Gender + "\r\n";
    }

}
