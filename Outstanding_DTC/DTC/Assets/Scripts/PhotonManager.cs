using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [Tooltip("The gameobject that is instantiated when someone joins the room")]    
    public GameObject avatarPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connected: " + PhotonNetwork.IsConnected);
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");
        RoomOptions roomOpt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("ApplicationRoom", roomOpt, new TypedLobby("ApplicationLobby", LobbyType.Default));
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room with " + PhotonNetwork.CurrentRoom.PlayerCount + " participants.");
        PhotonNetwork.Instantiate(avatarPrefab.name, new Vector3(), Quaternion.identity, 0);
    }
}
