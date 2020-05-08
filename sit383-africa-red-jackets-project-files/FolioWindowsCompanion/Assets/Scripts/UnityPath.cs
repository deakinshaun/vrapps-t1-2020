using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UnityPath : MonoBehaviour
{
	public string unityPath = "<no path>";
	
	public void onClick()
	{
		selectFile();
	}
	
	public void selectFile()
	{
		unityPath = EditorUtility.OpenFilePanel("Select Unity executable","","");
		Debug.Log("Folio will be created with: \n" + unityPath);
		
		if(unityPath.Length > 0)
		{
			Text _unityPath = GameObject.Find("Canvas/TxtUnityPath").GetComponent<Text>();
			_unityPath.text = "Unity.exe Path: " + unityPath;
		}
	}
}