using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class ControlAvatar : MonoBehaviour
{
    private bool is_owner = false;
    Vector3 mousePos;
    Vector3 pos;

    private void Start()
    {
        if (GetComponent<PhotonView>().IsMine == true || PhotonNetwork.IsConnected == false)
        {
           is_owner = true;
           pos = this.transform.position;
        }
            
    }


    private void Update()
    {
        if (is_owner == true)
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = .5f;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

    }

}
