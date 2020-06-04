using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class Client {

    private Socket clientSocket;
    private Thread t;
    private byte[] datas = new byte[1024];

    
    public Client(Socket s)
    {
        clientSocket = s;

        Thread t = new Thread(ReceiveMessage);
        t.Start();
    }

    private void ReceiveMessage()
    {
        while (true)
        {
            //判断是否断开连接，如果超过10毫秒没有读取到消息则与客户端断开
            if (clientSocket.Poll(10, SelectMode.SelectRead))
            {
                clientSocket.Close();
                break;
            }

            int length = clientSocket.Receive(datas);

            string message = Encoding.UTF8.GetString(datas, 0, length);

            GameManager.instance.BroadCastMessages(message);

            Debug.Log("Receive a message:" + message);
        }
    }
    //向每个客户端发消息
    public void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        clientSocket.Send(data);
    }
    //（属性）判断已连接的客户端是否断开
    public bool Connected
    {
        get { return clientSocket.Connected; }
    }
}
