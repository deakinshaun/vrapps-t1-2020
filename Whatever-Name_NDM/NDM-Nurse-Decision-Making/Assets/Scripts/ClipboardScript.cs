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

    // Start is called before the first frame update
    void Start()
    {
        ClipboardTextbox.text = "Name: " + PatientName + "\r\n" +
                                "Gender: " + PatientGender + "\r\n" +
                                "Age: " + PatientAge + "\r\n" +
                                "Allergies: " + PatientAllergies + "\r\n" +
                                "Health Conditions: " + PatientHealthConditions + "\r\n";

    }
}
