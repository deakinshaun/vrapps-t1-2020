using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject avatarPrefab;

    void Start()
    {
        Debug.Log("Initializing connection");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connection Established.");
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("SurgeryRoom1", roomopt, new TypedLobby("NDMLobby", LobbyType.Default));
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You are in a room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other participants. Ugheyeuh.");
        PhotonNetwork.Instantiate(avatarPrefab.name, new Vector3(), Quaternion.identity, 0);
    }
}