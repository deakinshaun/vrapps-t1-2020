using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;
using ExitGames.Client.Photon;

public class TrackManager : MonoBehaviourPunCallbacks
{
    public TextMesh roomLabel;
    public GameObject avatarPrefab;
    public TextMesh messageBoard;

    public override void OnJoinedRoom()
    {
        if (roomLabel != null)
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                roomLabel.text = "Room:\n" + PhotonNetwork.CurrentRoom.Name;
            }
        }
        base.OnJoinedRoom();

        Room r = PhotonNetwork.CurrentRoom;
        ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
        p["talk"] += "Room Started";

        PhotonNetwork.Instantiate(avatarPrefab.name, new Vector3(), Quaternion.identity, 0);
        OnRoomPropertiesUpdate(PhotonNetwork.CurrentRoom.CustomProperties);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("TeacherView");
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("Room property update");
        int n = 10;
        //Debug.Log("" + propertiesThatChanged["Talk"]);
        messageBoard.text = string.Join(Environment.NewLine,
            ((string)propertiesThatChanged["talk"]).Split(Environment.NewLine.ToCharArray()).Reverse().Take(n).Reverse().ToArray());
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
    }
}
