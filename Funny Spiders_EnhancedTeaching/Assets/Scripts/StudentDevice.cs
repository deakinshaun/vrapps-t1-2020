using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class StudentDevice : MonoBehaviour
{
    public GPS GPSLocation;
    // Start is called before the first frame update
    private void setButtonCallbacks()
    {
        //GameObject.Find("Canvas/TalkButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { Talk(); });
        //GameObject.Find("Canvas/LobbyButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { Lobby(); });
        GameObject.Find("Canvas/NickNameButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { Nickname(); });
    }

    public void Start()
    {

        if (GetComponent<PhotonView>().IsMine == true || PhotonNetwork.IsConnected == false)
        {
            setButtonCallbacks();
            //transform.Find("Camera").gameObject.SetActive(true);
        }
        Debug.Log(ClassRoomManager.getName(this.gameObject));
        GetComponent<PhotonView>().RPC("showNickname", RpcTarget.All, ClassRoomManager.getName(this.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine == true || PhotonNetwork.IsConnected == false)
        {
            GPSCalculation();
            //transform.rotation *= Quaternion.AngleAxis(turn * turnSpeed * Time.deltaTime, Vector3.up);
            ////transform.rotation *= Quaternion.AngleAxis(viewturn * turnSpeed * Time.deltaTime, Vector3.right);
            //transform.position += move * moveSpeed * transform.forward;
            ////GameObject.Find("PlayerID").transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z); 
        }
    }


    //public void turnLeft()
    //{
    //    turn = -1.0f;
    //}
    //public void turnRight()
    //{
    //    turn = 1.0f;
    //}
    ////public void ViewUp()
    ////{
    ////    viewturn = -1.0f;
    ////}
    ////public void ViewDown()
    ////{
    ////    viewturn = 1.0f;
    ////}
    //public void moveForward()
    //{
    //    move = 1.0f;
    //}
    //public void moveBack()
    //{
    //    move = -1.0f;
    //}
    //public void stopEverything()
    //{
    //    move = 0.0f;
    //    turn = 0.0f;
    //    //viewturn = 0.0f;
    //}

    public void GPSCalculation()
    {
        if (GPSLocation != null)
        {
            Room r = PhotonNetwork.CurrentRoom;
            ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
            p["talk"] += ClassRoomManager.getName(this.gameObject) + ":"
                + Time.time + ":" + GPSLocation.GetComponent<Text>().text + "\n";
            r.SetCustomProperties(p);
        }
    }


    //public void Talk()
    //{
    //    GameObject t = GameObject.Find("Canvas/TalkMessage/Text");
    //    if (t != null)
    //    {

    //        Room r = PhotonNetwork.CurrentRoom;
    //        ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
    //        p["talk"] += ClassRoomManager.getName(this.gameObject) + ":"
    //            + Time.time + ":" + t.GetComponent<Text>().text + "\n";
    //        r.SetCustomProperties(p);
    //    }
    //}

    //public void Lobby()
    //{
    //    GameObject t = GameObject.Find("Canvas/LobbyMessage/Text");
    //    if (t != null)
    //    {
    //        Room r = PhotonNetwork.CurrentRoom;
    //        ExitGames.Client.Photon.Hashtable p = r.CustomProperties;
    //        p["notices"] += ClassRoomManager.getName(this.gameObject) + ":" + Time.time + ":" + t.GetComponent<Text>().text + "\n";
    //        r.SetCustomProperties(p);
    //    }
    //}
    [PunRPC]

    void showNickname(string name)
    {
        Debug.Log(name);
        transform.Find("StudentName").gameObject.GetComponent<TextMesh>().text = name;
    }
    public void Nickname()
    {
        GameObject t = GameObject.Find("Canvas/NickNameName/Text");
        if (t != null)
        {
            GetComponent<PhotonView>().Owner.NickName = t.GetComponent<Text>().text;
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!My Nick Name: " + GetComponent<PhotonView>().Owner.NickName);
            GetComponent<PhotonView>().RPC("showNickname", RpcTarget.All, ClassRoomManager.getName(this.gameObject));
        }
    }
}
