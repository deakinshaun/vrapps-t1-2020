using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#pragma warning disable 0414
public class SocketFrameworkUse : MonoBehaviour {

    public static SocketFrameworkUse Instant;


    public InputField ipField;
    public InputField portField;

    public GameObject ServerControl;
    public GameObject PortControl;
    //网络状态（0表示未连接，1表示服务器，2表示客户端）
    int networkState = 0;

    #region UI变量

    string clientConnectIP = "192.168.1.105";
    string clientConnectPort = "23456";
    string msgText = "";
    public string currentMsg = "";

    #endregion

    void Awake()
    {
        Instant = this;

    }

    void Start()
    {
        Application.runInBackground = true;
    }

    void MsgCallback(string msg)
    {
        currentMsg += msg + "\n";
    }


    // 我是服务端调按钮回调

    public void ServerBnt()
    {

        // 初始化服务器
        SocketFramework.GetIns().ServerInit(MsgCallback);

        // 跳到服务端界面

        ServerControl.SetActive(true);
        gameObject.SetActive(false);

    }

    // 我是接收端按钮回调方法
    public void PortBtn()
    {
        clientConnectIP = ipField.text;
        clientConnectPort = portField.text;
        SocketFramework.GetIns().ClientConnect(clientConnectIP,
            int.Parse(clientConnectPort), MsgCallback);

        PortControl.SetActive(true);
        gameObject.SetActive(false);
    }

}
