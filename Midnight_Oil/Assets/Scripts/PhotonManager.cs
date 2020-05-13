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
        GameObject player;
        Debug.Log("You are in the room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other participants. You see a grue." + "\n" + PhotonNetwork.PlayerList);
        player =  PhotonNetwork.Instantiate(avatarPrefab.name, avatarPrefab.transform.position, Quaternion.identity,0);
        Color colour = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        ChangeColour(player, colour);

    }

    public void ChangeColour (GameObject p, Color c )
    {
        p.GetComponentInChildren<Renderer>().material.SetColor("_Color", c);
    }

    // Update is called once per frame

    void Update()
    {
        
    }
} 
