using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LocalRoomBehaviour : MonoBehaviour
{
    private ClassRoomManager roomManager;

    public void setManager (ClassRoomManager manager)
    {
        roomManager = manager;
    }

    public void enterRoom ()
    {
        string roomName = GetComponent<DisplayRoom>().getName();
        Debug.Log("Entering name: " + roomName);
        roomManager.JoinRoom(roomName);
    }
}
