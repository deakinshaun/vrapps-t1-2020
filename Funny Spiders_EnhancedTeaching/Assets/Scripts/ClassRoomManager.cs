using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ClassRoomManager : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    public Canvas roomCanvas;
    //public GameObject LoginPage;
    private bool allowingJoining = false;

    List<GameObject> displayRooms = new List<GameObject>();

    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public static string getName(GameObject o)
    {
        Debug.Log("Step 1: " + o.GetComponent<PhotonView>());

        if (o.GetComponent<PhotonView>() != null)
        {
            Debug.Log("Step 2: " + o.GetComponent<PhotonView>().Owner);
            //Debug.Log("Step 3: " + o.GetComponent<PhotonView>().Owner.NickName);
            if ((o.GetComponent<PhotonView>().Owner.NickName != null) && !(o.GetComponent<PhotonView>().Owner.NickName.Equals("")))
            {

                return o.GetComponent<PhotonView>().Owner.NickName;
            }
            else
            {
                return o.GetComponent<PhotonView>().Owner.UserId;
            }
        }
        else
        {
            return "X" + PhotonNetwork.AuthValues.UserId;
        }
    }

    GameObject getRoomObject(string name)
    {
        foreach (GameObject g in displayRooms)
        {
            DisplayRoom dr = g.GetComponent<DisplayRoom>();
            if (dr.getName().Equals(name))
            {
                return g;
            }
        }
        GameObject room = Instantiate(roomPrefab);
        room.transform.SetParent(roomCanvas.transform);
        room.GetComponent<DisplayRoom>().setName(name);
        room.GetComponent<LocalRoomBehaviour>().setManager(this);
        displayRooms.Add(room);
        return room;
    }

    void removeRoomObject(GameObject room)
    {
        displayRooms.Remove(room);
        Destroy(room);
    }

    void updateRooms()
    {
        int row = 0;
        int col = 0;
        int columnLimit = 2;
        foreach (GameObject room in displayRooms)
        {
            room.transform.localPosition = new Vector3(col * 300 - 100, row * 100, 0);
            col += 1;
            if (col >= columnLimit)
            {
                col = 0;
                row -= 1;
            }
        }
    }

    public void JoinRoom(string roomName)
    {
        allowingJoining = true;
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("------------- JoinRoom() Function");
        PhotonNetwork.LoadLevel("TeacherTrackingArea");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo ri in roomList)
        {
            GameObject room = getRoomObject(ri.Name);
            if (ri.RemovedFromList)
            {
                removeRoomObject(room);
            }
            else
            {
                room.GetComponent<DisplayRoom>().display(ri.Name + "\n\nwith " + ri.PlayerCount + "players\n" + ri.CustomProperties["notices"]);
            }
        }
        updateRooms();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Room r = PhotonNetwork.CurrentRoom;
        ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
        p["notices"] = ClassRoomManager.getName(this.gameObject) + ": " + Time.time + ": joined\n";
        r.SetCustomProperties(p);

        if (!allowingJoining)
        {
            PhotonNetwork.LeaveRoom();
        }
        //LoginPage.SetActive(false);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room " + returnCode + " " + message);
    }

    public void addRoom(Text name)
    {
        Debug.Log("Adding new room: " + name.text);
        RoomOptions ro = new RoomOptions();
        ro.EmptyRoomTtl = 100000;

        string[] roomPropsInLobby = { "notices" };
        ro.CustomRoomPropertiesForLobby = roomPropsInLobby;

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            { "notices", "Room Start\n" }
        };
        ro.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.JoinOrCreateRoom(name.text, ro, null);
    }
}
