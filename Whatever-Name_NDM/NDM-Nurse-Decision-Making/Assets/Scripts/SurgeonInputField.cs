using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurgeonInputField : MonoBehaviour
{

    public GameObject NameBox;
    public GameObject GenderBox;
    public GameObject AgeBox;
    public GameObject AllergiesBox;
    public GameObject HistoryBox;

    // Start is called before the first frame update
    void Start()
    {

        var NameInput = NameBox.GetComponent<InputField>();
        NameInput.onEndEdit.AddListener(NameChange);

        var GenderInput = GenderBox.GetComponent<InputField>();
        GenderInput.onEndEdit.AddListener(GenderChange);

        var AgeInput = AgeBox.GetComponent<InputField>();
        AgeInput.onEndEdit.AddListener(AgeChange);

        var AllergiesInput = AllergiesBox.GetComponent<InputField>();
        AllergiesInput.onEndEdit.AddListener(AllergiesChange);

        var HistoryInput = HistoryBox.GetComponent<InputField>();
        HistoryInput.onEndEdit.AddListener(HistoryChange);

    }
    //a little bit about the naming scheme here, *****Input means the arbtirary variable
    //to hold whatever's in the input field

    //Input**** is the string which gets handed to the change functions

    private void NameChange(string InputName)
    {
        GameObject NurseClipboard = GameObject.Find("Scroll View");
        ClipboardScript clipboardScriptEdit = NurseClipboard.GetComponent<ClipboardScript>();
        //fetching the clipboard script to access its values. Use Clipboardscriptedit to edit values
        clipboardScriptEdit.PatientName = InputName;
    }
    private void GenderChange(string InputGender)
    {
        GameObject NurseClipboard = GameObject.Find("Scroll View");
        ClipboardScript clipboardScriptEdit = NurseClipboard.GetComponent<ClipboardScript>();
        //fetching the clipboard script to access its values. Use Clipboardscriptedit to edit values
        clipboardScriptEdit.PatientGender = InputGender;
    }
    private void AgeChange(string InputAge)
    {
        GameObject NurseClipboard = GameObject.Find("Scroll View");
        ClipboardScript clipboardScriptEdit = NurseClipboard.GetComponent<ClipboardScript>();
        //fetching the clipboard script to access its values. Use Clipboardscriptedit to edit values
        clipboardScriptEdit.PatientAge = InputAge;
    }
    private void AllergiesChange(string InputAllergies)
    {
        GameObject NurseClipboard = GameObject.Find("Scroll View");
        ClipboardScript clipboardScriptEdit = NurseClipboard.GetComponent<ClipboardScript>();
        //fetching the clipboard script to access its values. Use Clipboardscriptedit to edit values
        clipboardScriptEdit.PatientAllergies = InputAllergies;
    }
    private void HistoryChange(string InputHistory)
    {
        GameObject NurseClipboard = GameObject.Find("Scroll View");
        ClipboardScript clipboardScriptEdit = NurseClipboard.GetComponent<ClipboardScript>();
        //fetching the clipboard script to access its values. Use Clipboardscriptedit to edit values
        clipboardScriptEdit.PatientHealthConditions = InputHistory;
    }


    //same popup script as before
    private bool PopupEnabled = false;

    private void PopupTrue()
    {
        PopupEnabled = true;
        Debug.Log("Popup Enabled =" + PopupEnabled);
    }

    private void PopupFalse()
    {
        PopupEnabled = false;
        Debug.Log("Popup Enabled =" + PopupEnabled);
    }

    public void Popup()
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
            if (transform.position.y < -0.8881858f)
            {
                transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            }
        }

        if (PopupEnabled == false)
        {
            if (transform.position.y > -1.935408f)
            {
                transform.Translate(Vector3.down * Time.deltaTime, Space.World);
            }
        }
    }

}
