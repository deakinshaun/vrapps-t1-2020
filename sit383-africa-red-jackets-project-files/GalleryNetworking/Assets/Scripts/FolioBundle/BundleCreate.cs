using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BundleCreate : MonoBehaviour
{
	public string 	user = "hrydell";			// temp
	public int 		numOfArtefacts = 2;			// temp, update based on number of models uploaded by the user
	
	public void onClick() {
		CreateAssetBundles();
	}
	
	static void CreateAssetBundles() {
		BuildPipeline.BuildAssetBundles("Assets/Folios", 
			BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android); 	// builds bundle and compresses it. Can build target for StandaloneWindows or StandaloneWindows64
		Debug.Log("Bundles built for Android platform");
	}
	
/*	1. Need to get models in from an external location
		Open model through explorer
		Create list
		Confirmation
	2. Turn the collected models into a Bundle
	3. Create a bundle
	4. Store the bundle somewhere
	5. Access the bundle between sessions
	
	Asset Bundles cannot be created during runtime.
	

*/
	
	
}
