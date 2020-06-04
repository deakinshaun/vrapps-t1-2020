using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LogInPanel : MonoBehaviour {

    public InputField nameInput;
    public InputField ipInput;
    public InputField portInput;

    public NetworkManager networkManager;

    public GameObject teacherPrefab;
    //public GameObject studentprefab;

    public GameObject network;

    public bool isConnect = false;

    public GameObject chat_client;

    public GameObject chat_image;

    private void Start()
    {
        network.SetActive(false);
        chat_client.SetActive(false);
        chat_image.SetActive(false);
    }
    //点击teacher时执行
    public void OnClickServer()
    {
        Info.username = nameInput.text;
        Info.userIp = ipInput.text;
        Info.userPort = portInput.text;

        networkManager.playerPrefab = teacherPrefab;

        network.SetActive(true);
        chat_client.SetActive(true);
        this.gameObject.SetActive(false);

        chat_image.SetActive(true);
    }
    //点击client时执行
    public void OnClickClient()
    {
        Info.username = nameInput.text;
        Info.userIp = ipInput.text;
        Info.userPort = portInput.text;

        //networkManager.playerPrefab = studentprefab;

        network.SetActive(true);
        chat_client.SetActive(true);
        this.gameObject.SetActive(false);

        chat_image.SetActive(true);
    }
    //点击弹出对话框
    public void OnClickChatImage()
    {
        chat_client.SetActive(true);
    }
    //点击关闭对话框
    public void OnClickClose()
    {
        chat_client.SetActive(false);
    }
}
