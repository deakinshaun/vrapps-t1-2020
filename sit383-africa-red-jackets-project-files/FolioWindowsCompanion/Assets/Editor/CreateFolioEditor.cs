using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

// doc: https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html
public class CreateFolioEditor : AssetPostprocessor
{ 	
	static void OnPreprocessAsset()
	{	
		// if we're not in batchmode, exit script
		if(!Application.isBatchMode) return;
		
		bool 		_debug 		= false;					// turn on to open log 
		string 		_folioPath 	= "Assets/MyTempAssets", 
					_file 		= "", 
					_username 	= "hrydell";
		string[]	_files 		= Directory.GetFiles(_folioPath);
		
		if(_files.Length != 0)											// if files exist
		{
			for(int i = 0; i < _files.Length; i++)						// for each one
			{
				if(_debug) Debug.Log("HAYDEN IT'S ME! The file being currently looked at is: " + _files[i]);
				
				if(_files[i].EndsWith(".png") || _files[i].EndsWith(".jpg")) 
				{					
					_file = _files[i].Replace(@"/", @"\");				// replace slashes
					AssetImporter.GetAtPath(_file).SetAssetBundleNameAndVariant(_username + "'s folio", "fd");			// should save it as 'user's folio.fd'
					
					if(_debug) Debug.Log("HAYDEN IT'S ME AGAIN!! The file that was just updated was: " + _file + "\nand it was added to: " + _username + "'s folio.fd");
				}
			}
		}
		
		if(_debug) 
		{
			string _logPath = "C:/Users/hayde/AppData/Local/Unity/Editor/Editor.log";
			Application.OpenURL(_logPath);
		}
		
		/*
		string _outputPath = "Assets/LocalFolioDB";
		
		// Build AssetBundle
		if(!System.IO.Directory.Exists(_outputPath)) 
			System.IO.Directory.CreateDirectory(_outputPath);	// make sure dir exists
		
		BuildPipeline.BuildAssetBundles(_outputPath, BuildAssetBundleOptions.None, BuildTarget.Android);
		Debug.Log("HAYDEN, YAHELLO IT'S ME ONCE MORE!!!!!! THE FILES CREATED A BUNDLE AT: " + _outputPath);
		*/
	} 
}
