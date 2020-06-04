﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerControl : MonoBehaviour
{

    public InputField sendField;
    //public UITextList textList;

    void Awake()
    {
        gameObject.SetActive(false);

    }

    // 服务端发送消息按钮回调
    public void SendMessage()
    {
        string msgText = sendField.text;
        SocketFramework.GetIns().ServerSend(msgText);
        //textList.Add(SocketFrameworkUse.Instant.currentMsg);
    }

    void OnGUI()
    {
        //显示所有消息
        GUILayout.Label(SocketFrameworkUse.Instant.currentMsg);

    }

}
