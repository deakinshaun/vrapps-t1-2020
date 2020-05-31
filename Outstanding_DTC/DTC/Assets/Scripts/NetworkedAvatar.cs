using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice;

using OVRTouchSample;

public class NetworkedAvatar : MonoBehaviourPunCallbacks, IPunObservable
{
    private GameObject ovrCameraRig;
    
    // the transforms on this networked avatar that get synched to the player controller
    public Transform head;
    public Transform handRight;
    public Transform handLeft;

    // the transforms for the source data being tracked by ovr player controller
    private Transform handRightSource;
    private Transform headSource;
    private Transform handLeftSource;

    // for future reference if we need to sync other data across networked clients
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Networked player instantiated.");
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            // assign necessary links to the source data
            ovrCameraRig = GameObject.Find("OVRCameraRig");
            OVRCameraRig cameraRig = ovrCameraRig.GetComponent<OVRCameraRig>();
            headSource = cameraRig.centerEyeAnchor;
            handLeftSource = cameraRig.leftHandAnchor;
            handRightSource = cameraRig.rightHandAnchor;
        }
        else if (!photonView.IsMine)
        {
            // disable all the oculus touch hand code if this isn't my avatar
            // ensures hand gestures aren't manipulated by other clients
            Hand[] hands = GetComponentsInChildren<Hand>();
            foreach (Hand hand in hands)
            {
                hand.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            UpdateTransforms();
        }
    }

    private void UpdateTransforms()
    {
        // update positions of body, head, left and right hands if this is mine
        // this is global as it's the main player position in the world
        transform.position = ovrCameraRig.transform.position;
        transform.rotation = ovrCameraRig.transform.rotation;

        // these are local as they are determined relative to the main player's global position
        head.localPosition = headSource.localPosition;
        head.localRotation = headSource.localRotation;
        handLeft.localPosition = handLeftSource.localPosition;
        handLeft.localRotation = handLeftSource.localRotation;
        handRight.localPosition = handRightSource.localPosition;
        handRight.localRotation = handRightSource.localRotation;
    }
}
