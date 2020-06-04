using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System.Threading;

public class ChatManager : MonoBehaviour {

    private string ipAdress = "192.168.1.105";
    private int port = 7788;
    private Socket clientSocket;
    private Thread t;
    private byte[] datas = new byte[1024];
    private string messages = "";
    private string oldMessage;

    public InputField inputText;
    public Text showText;
    public Text tipText;

    private void Start()
    {
        tipText.gameObject.SetActive(false);

        ipAdress = Info.userIp;
        port = int.Parse(Info.userPort);
        ConnectToServer();
    }
    private void Update()
    {
        if (messages != null && messages.Trim() != "")
        {
            showText.text += "\n" + messages;

            if (messages != oldMessage)
            {
                ShowTipText();
            }

            messages = "";
        }
    }
    //显示提示文本
    private void ShowTipText()
    {
        tipText.gameObject.SetActive(true);
        tipText.text = "已收到一个新信息";
        StartCoroutine(Fade());
    }
    private void ConnectToServer()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //连接到服务器
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ipAdress), port));
        //创建一个新的线程来接收消息
        t = new Thread(ReceiveMessages);
        t.Start();
    }
    //向服务器端发信息
    private void SendMessageToServer(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(Info.username+":"+message);
        clientSocket.Send(data);
    }
    //按钮点击事件，点击调用SendMessageToServer发送信息
    public void OnClickSendMessage()
    {
        string value = inputText.text;
        oldMessage = Info.username + ":" + value;
        SendMessageToServer(value);
        inputText.text = "";
    }
    //系统方法，在退出时执行,使socket断开
    private void OnDestroy()
    {
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
    //退出时调用
    private void OnApplicationQuit()
    {
        OnDestroy();
    }
    //接受服务器端广播过来的消息
    private void ReceiveMessages()
    {
        while (true)
        {
            if (clientSocket.Connected == false)
                return;

            int length = clientSocket.Receive(datas);
            messages = Encoding.UTF8.GetString(datas,0,length);
            //showText.text += "\n" + messages;
        }
    }
    //迭代器方法，在1秒后让提示文本消失
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1);
        tipText.gameObject.SetActive(false);
    }
}
