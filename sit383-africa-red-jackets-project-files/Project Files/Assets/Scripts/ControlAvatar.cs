using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun; //Allows use of Photon PUN

public class ControlAvatar : MonoBehaviour
{
    //Initialise move and turn values
    private float move = 0.0f;
    private float turn = 0.0f;

    public float turnSpeed = 100.0f;
    public float moveSpeed = 0.1f;

    void Start()
    {
        if(GetComponent<PhotonView>().IsMine == true || PhotonNetwork.IsConnected == false)
        {
            //Finds the Canvas object, and its children; e.g. LeftButton; then grabs the method and adds a Event Listener for , e.g. turnLeft()
            //NOTE!! The triggers[n] refers to the triggers added earlier (PressDown and PressUp)
            GameObject.Find("Canvas/LeftButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { turnLeft(); });
            GameObject.Find("Canvas/LeftButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });

            GameObject.Find("Canvas/RightButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { turnRight(); });
            GameObject.Find("Canvas/RightButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });

            GameObject.Find("Canvas/ForwardButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { moveForward(); });
            GameObject.Find("Canvas/ForwardButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });

            GameObject.Find("Canvas/BackwardButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { moveBackward(); });
            GameObject.Find("Canvas/BackwardButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });

            //Set Camera to follow player, as player is thereby the local player (from photon documentation at https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/pun-basics-tutorial/player-camera-work)
            CameraManager localCamera = this.gameObject.GetComponent<CameraManager>();

            if(localCamera != null)
            {
                localCamera.OnStartFollowing();
            }
            else
            {
                Debug.LogError("Missing CameraManager component on player!");
            }
        }
    }

    void Update()
    {
        //Set Avatar turning based on values
        transform.rotation *= Quaternion.AngleAxis(turn * turnSpeed * Time.deltaTime, Vector3.up);
        //Set Avatar movement based on values
        transform.position += move * moveSpeed * transform.forward;
    }

    public void turnLeft()
    {
        turn = -1.0f;
    }

    public void turnRight()
    {
        turn = 1.0f;
    }

    public void moveForward()
    {
        move = 1.0f;
    }

    public void moveBackward()
    {
        move = -1.0f;
    }

    public void stopEverything()
    {
        turn = 0.0f;
        move = 0.0f;
    }
}
