using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using UnityEngine.UI;
using System.Text;
using System.Threading;

#pragma warning disable 0649
public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public InputField textInput;
    public Text showText;
    
    private List<Client> clientList = new List<Client>();

    private Socket tcpServer;
    private byte[] data;
    private string message = "";
    private Thread t;

    private Socket currentSocket;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        tcpServer.Bind(new IPEndPoint(IPAddress.Parse("192.168.1.105"), 7788));

        tcpServer.Listen(100);

        //创建一个新的线程来接收消息
        t = new Thread(ReceiveMessageToClient);
        t.Start();

        //while (true)
        //{
        //    Socket clientSocket = tcpServer.Accept();
        //    Client client = new Client(clientSocket);
        //    clientList.Add(client);
        //}
    }
    private void Update()
    {
        if (message != null && message.Trim() != "")
        {
            showText.text += message;
            message = "";
        }

        while (tcpServer.Accept()!=null)
        {
            if (tcpServer.Accept() == currentSocket)
                break;
            currentSocket = tcpServer.Accept();
            Socket clientSocket = tcpServer.Accept();
            Client client = new Client(clientSocket);
            clientList.Add(client);
        }

        
        print(clientList.Count);
    }
    //向每个客户端广播消息
    public void BroadCastMessages(string message)
    {
        var notConnectedList = new List<Client>();
        //遍历已连接的客户端并向其发送信息
        //如果没有连接则加入notConnectedList中
        foreach (var client in clientList)
        {
            if (client.Connected)
            {
                client.SendMessage(message);
            }
            else
            {
                notConnectedList.Add(client);
            } 
        }
        //移除已断开的客户端
        foreach (var notConnected in notConnectedList)
        {
            clientList.Remove(notConnected);
        }
    }
    //向客户端发信息
    public void SendMessageToClient()
    {
        string data = textInput.text;
        BroadcastMessage(Info.username+":"+data);
        textInput.text = "";
    }
    //接受客户端消息
    public void ReceiveMessageToClient()
    {
        while (true)
        {
            if (tcpServer.Connected == false)
                return;

            int length = tcpServer.Receive(data);
            message = Encoding.UTF8.GetString(data, 0, length);
        }
    }
}
