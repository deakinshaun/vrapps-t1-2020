using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Allows use of canvas and UI elements, including text

using Photon.Pun; //Allows use of Photon PUN
using Photon.Realtime; //Photon Realtime functionality import

public class PhotonManager : MonoBehaviourPunCallbacks //Photon-enhanced object
{
    public GameObject avatarPrefab;
    public Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Photon manager starting...");
        debugText.text += "\n" + "Photon manager starting...";
        PhotonNetwork.ConnectUsingSettings(); //Test connection using photon connection settings; displays region connected to
        //NOTE!! If not connected to the same region, two users cannot join the same room
    }

    //Override Photon OnConnected method in order to apply our own functionality
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected successfully.");
        debugText.text += "\n" + "Connected successfully.";
        RoomOptions roomopt = new RoomOptions(); //Alows us to come back and tweak the room functions later
        PhotonNetwork.JoinOrCreateRoom("ApplicationRoom", roomopt, new TypedLobby("ApplicationLobby", LobbyType.Default));
    }

    public override void OnJoinedRoom()
    {
        //Prints the number of users in the room
        Debug.Log("You are in a room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other participants.");
        debugText.text += "\n" + "You are in a room with " + PhotonNetwork.CurrentRoom.PlayerCount + " other participants.";
        PhotonNetwork.Instantiate(avatarPrefab.name, new Vector3(), Quaternion.identity, 0); //Instantiate the Avatar object on the network; thus visible to everyone in the room
    }
}
