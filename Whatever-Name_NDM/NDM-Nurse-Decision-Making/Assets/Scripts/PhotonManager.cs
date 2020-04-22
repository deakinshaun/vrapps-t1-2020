using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject avatarPrefab1;
    public GameObject avatarPrefab2;

    public string RoleName;

    public void AssignRoleNurse()
    {
        RoleName = "Nurse";
        Debug.Log("Role Selected: Nurse");
    }

    public void AssignRoleSurgeon()
    {
        RoleName = "Surgeon";
        Debug.Log("Role Selected: Surgeon");
    }

    public void initialize()
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
        if (RoleName.Equals("Nurse"))
        {
            PhotonNetwork.Instantiate(avatarPrefab1.name, new Vector3(), Quaternion.identity, 0);
        }
        if (RoleName.Equals("Surgeon"))
        {
            PhotonNetwork.Instantiate(avatarPrefab2.name, new Vector3(), Quaternion.identity, 0);
        }
    }
}