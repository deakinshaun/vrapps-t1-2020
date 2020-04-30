using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

public class ControlAvatar : MonoBehaviour
{
    private float move = 0.0f;
    private float turn = 0.0f;

    public float turnSpeed = 100.0f;
    public float moveSpeed = 1.0f;

    private void Start()
    {
        if (GetComponent<PhotonView>().IsMine == true || PhotonNetwork.IsConnected == false)
        { 
            GameObject.Find("Movement_UI/LeftButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { turnLeft(); });
            GameObject.Find("Movement_UI/LeftButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });

            GameObject.Find("Movement_UI/RightButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { turnRight(); });
            GameObject.Find("Movement_UI/RightButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });

            GameObject.Find("Movement_UI/ForwardButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { moveForward(); });
            GameObject.Find("Movement_UI/ForwardButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });

            GameObject.Find("Movement_UI/BackwardButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { moveBackward(); });
            GameObject.Find("Movement_UI/BackwardButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stopEverything(); });
        }
        
    }

    private void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(turn * turnSpeed * Time.deltaTime, Vector3.up);
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
        move = -0.05f;
    }
    public void moveBackward()
    {
        move = 0.05f;
    }
    public void stopEverything()
    {
        move = 0.0f;
        turn = 0.0f;
    }
}

