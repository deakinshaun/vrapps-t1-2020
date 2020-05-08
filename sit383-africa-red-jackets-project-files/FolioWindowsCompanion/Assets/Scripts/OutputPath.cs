using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// --- Functionally this works, but until a bundle can be created
// --- there's no point working out an output path
public class OutputPath : MonoBehaviour
{
	public string outputPath = "<no path>";
	
	public void onClick()
	{
		return;			// just cancel it for now
		
		selectFolder();
	}
	
	public void selectFolder()
	{		
		outputPath = EditorUtility.OpenFolderPanel("Select output folder","","");
		Debug.Log("Folio will be created at: \n" + outputPath);
		
		if(outputPath.Length > 0)
		{
			Text _outputPath = GameObject.Find("Canvas/TxtOutputPath").GetComponent<Text>();
			_outputPath.text = "Output Path: " + outputPath;
		}
	}
}