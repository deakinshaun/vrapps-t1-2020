using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameObject avatarPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Photon manager starting");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected. Yay!");
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("ApplicationRoom", roomopt, new TypedLobby("ApplicationLobby", LobbyType.Default));
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other participants. You see a grue." + "\n" + PhotonNetwork.PlayerList);
        PhotonNetwork.Instantiate(avatarPrefab.name, avatarPrefab.transform.position, Quaternion.identity,0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
} 
