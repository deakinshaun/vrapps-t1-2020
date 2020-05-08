using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleLoad : MonoBehaviour
{
	AssetBundle loadedBundle;
	public string path;
	
    void Start() {
        LoadAssetBundle(path);
    }
	
	void LoadAssetBundle(string path) {
		loadedBundle = AssetBundle.LoadFromFile(path);
		
		Debug.Log(loadedBundle == null ? "failed to load" : "loaded successfully");
	}
}
