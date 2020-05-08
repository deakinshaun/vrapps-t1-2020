using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;		// errors involving this.
using UnityEditor;

public class CreateFolio : MonoBehaviour
{
	string batPath;
	//string folioPath = "Assets/MyTempAssets";
	
	public void onClick()
	{
		//string _outputPath = GameObject.Find("Canvas/BtnOutputPath").GetComponent<OutputPath>().outputPath;
		string _unityPath = GameObject.Find("Canvas/BtnUnityPath").GetComponent<UnityPath>().unityPath;
		string _startupPath = Application.dataPath;
		
		_startupPath = _startupPath.Replace("/Assets", "");						// get rid of Assets path from startupPath		
		batPath = Path.Combine(Application.dataPath, "CreateFolio.bat");		// get the path for application's exe and combine it with string
		
		updateBatch(_unityPath, _startupPath);
		launchBatch();
	}
	
	void updateBatch(string unityPath, string startupPath)
	{		
		if(System.IO.File.Exists(batPath)) System.IO.File.Delete(batPath);		// delete file if present
		
		// use 'using' so that the process is disposed on complete
		using(StreamWriter _sw = new StreamWriter(batPath))
		{	
			/* string is LITERAL, do NOT change its formatting
				delay for 5 seconds, let UnityEditor shut down 
				alternatively, could check for the program running and loop delay until it stops.
				-executeMethod does NOT accept parameters. It must be a static method.
			*/
			_sw.WriteLine(@"timeout 5 /nobreak 
""{0}"" -batchmode -quit -projectPath ""{1}"" -executeMethod CreateFolioEditor.OnPreprocessAsset", 
				unityPath, startupPath);
		}
	}
	
	void launchBatch()
	{
		if(System.IO.File.Exists(batPath)) 
		{
			Application.OpenURL(batPath);
			EditorApplication.Exit(0);
		}
	}
}
