using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float distance = -0.11f; //the extra 0.01 is to clear the nose (0.1 is exactly at the end)
    private float height = 1.5f;

    //private Vector3 centerOffset = Vector3.zero;
    private bool followOnStart = false;
    //private float smoothSpeed = 1.125f;
    Transform cameraTransform;
    bool isFollowing;
    Vector3 cameraOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if(followOnStart)
        {
            OnStartFollowing();
        }
    }

    void LateUpdate()
    {
        // The transform target may not destroy on level load,
        // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }

        // only follow is explicitly declared
        if (isFollowing)
        {
            Cut();
        }
    }

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        // we don't smooth anything, we go straight to the right camera shot
        Cut();
    }

    /*
    //Gradual transition for camera positon and view
    void Follow()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);

        cameraTransform.LookAt(this.transform.position + centerOffset);
    }
    */

    //Instant cut for camera position and view
    void Cut()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;

        //Set camera position to players
        cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);

        //Set camera rotation to players
        cameraTransform.rotation = this.transform.rotation;

        //cameraTransform.LookAt(this.transform.position + centerOffset);
    }
}
