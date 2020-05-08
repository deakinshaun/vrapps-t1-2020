using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]

public class ARController : MonoBehaviour {
	public 	GameObject 			objectToSpawn;
	public 	GameObject 			ARCamera;
	
	private GameObject			_spawnedObj;
	private ARRaycastManager 	_ARRaycastManager;
	private Vector2 			_touchPos;
	
	static 	List<ARRaycastHit> 	hits = new List<ARRaycastHit>();
	
	private void Awake() {
		_ARRaycastManager = GetComponent<ARRaycastManager>();
	}
	
	bool getTouchPosition(out Vector2 _touchPos) {
		if(Input.touchCount > 0) {
			_touchPos = Input.GetTouch(0).position;
			
			return true;
		}
		
		_touchPos = default;
		
		return false;
	}

    void Update() {
        if(!getTouchPosition(out Vector2 _touchPos)) return;
		
		if(_ARRaycastManager.Raycast(_touchPos, hits, TrackableType.PlaneWithinPolygon)) {
			var _hitPose = hits[0].pose;
			
			if(_spawnedObj == null) {
				Vector3 _cameraPosition = ARCamera.transform.position; 	// get position of camera
				
				_spawnedObj = Instantiate(objectToSpawn, _hitPose.position, _hitPose.rotation);
				_cameraPosition.y = _hitPose.position.y + 1.5f;				// spawn at our camera's y value | + 1.5f for howard test
				_spawnedObj.transform.LookAt(ARCamera.transform.position, _spawnedObj.transform.up);	// points camera at door
			}
			else {
				_spawnedObj.transform.position = _hitPose.position + new Vector3(0, 1.5f, 0);
				_spawnedObj.transform.rotation = _hitPose.rotation;				// set pos and rotation 
			}
		}
    }
}
