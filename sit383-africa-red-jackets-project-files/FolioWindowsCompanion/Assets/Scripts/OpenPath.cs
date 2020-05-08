using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class OpenPath : MonoBehaviour
{
	public string 			path = "<no path>";
	public List<string> 	filteredAssets = new List<string>();
	
    public void onClick()
    {		
        selectFolder();
		addFiles(path);
    }
	
	public void selectFolder() 
	{
		path = EditorUtility.OpenFolderPanel("Select Folio asset directory","","");
		Debug.Log("Folio directory selected as:\n" + path);	
		
		if(path.Length > 0)
		{	
			Text _folioPath = GameObject.Find("Canvas/TxtFolioPath").GetComponent<Text>();
			_folioPath.text = "Folio Path: " + path;
		}	
	}
	
	void addFiles(string path)
	{
		string 		_destPath = "Assets/MyTempAssets";						// where folio assets will go
		string[]	_unfilteredAssets = Directory.GetFiles(path);			// set array to all files in directory
		Scene 		_scene = SceneManager.GetActiveScene();
		
		if(_unfilteredAssets.Length != 0)
		{
			for(int i = 0; i < _unfilteredAssets.Length; i++)
			{
				string _file = _unfilteredAssets[i];		// unity likes to use / whereas explorer likes opposite
				_file = updateSlashToForwards(_file);
				
				if(_file.EndsWith(".png") || _file.EndsWith(".jpg"))
				{
					filteredAssets.Add(_file);
					Debug.Log("Added to filtered list: " + _file);
				}
			}
		}
		else Debug.Log("WARNING: this location does not contain any supported files");
		
		for(int i = 0; i < filteredAssets.Count; i++) 
		{	
			Debug.Log(filteredAssets[i]);
			FileUtil.CopyFileOrDirectory(filteredAssets[i], _destPath + "/" + Path.GetFileName(filteredAssets[i]));
		}
	}
	
	private string updateSlashToForwards(string path)
	{
		path = path.Replace(@"\", @"/");
		
		return path;
	}
	
	private string updateSlashToBackwards(string path)
	{
		path = path.Replace(@"/", @"\");
		
		return path;
	}
}
