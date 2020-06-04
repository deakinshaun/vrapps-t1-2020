using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

#pragma warning disable 0219
/// <summary>
/// 消息回调
/// </summary>
public delegate void SocketCallback(string msg);

public class SocketFramework : MonoBehaviour
{

    private static SocketFramework ins;

    public static SocketFramework GetIns()
    {
        if (ins == null)
        {
            ins = new SocketFramework();
        }

        return ins;
    }

    private SocketFramework() { }

    #region 服务器端

    //声明一个服务器套接字
    private Socket serverSocket;
    //服务器回调
    private SocketCallback serverCallback;
    //服务器消息缓存
    private byte[] serverBuffer;
    //已连接的Socket
    private Socket m_workingSocket;

    

    public void ServerInit(SocketCallback cb)
    {
        
        //定义Buffer长度
        serverBuffer = new byte[100];
        //接收回调
        serverCallback = cb;
        //1、初始化Socket
        serverSocket = new Socket(AddressFamily.InterNetwork/*地址族：IPV4*/,
            SocketType.Stream/*双向读写流*/, ProtocolType.Tcp/*传输层使用协议*/);
        //定义一个网络节点
        IPEndPoint ep = new IPEndPoint(IPAddress.Any/*选择已经连接到互联网的最优的网络地址*/, 23456);
        //2、绑定网络节点
        serverSocket.Bind(ep);
        //3、设置异步监听数量
        serverSocket.Listen(50);
        //向上层发送服务器创建成功的消息
        serverCallback("Server Has Init！！！");
        //4、异步接受客户端的连接请求
        serverSocket.BeginAccept(ServerAcceptCallback, serverSocket);
    }

    /// <summary>
    /// 异步接受消息的回调
    /// </summary>
    /// <param name="ar">Ar.</param>
    private void ServerAcceptCallback(System.IAsyncResult ar)
    {
        //获取当前的套接字
        Socket currentSocket = ar.AsyncState as Socket;
        //结束接受请求
        m_workingSocket = currentSocket.EndAccept(ar);
        ////向上层发送接受到客户端的消息
        serverCallback("Accept Client:" + m_workingSocket.LocalEndPoint);
        //5、开启异步接收客户端的消息
        m_workingSocket.BeginReceive(serverBuffer/*消息缓存*/, 0/*消息起始点*/, serverBuffer.Length/*消息长度*/
            , SocketFlags.None/*消息标志位*/, ServerReceiveCallback/*消息回调*/, m_workingSocket);
        //尾递归
        //4、异步接受客户端的连接请求
        serverSocket.BeginAccept(ServerAcceptCallback, serverSocket);
    }

    /// <summary>
    /// 异步接收消息的回调
    /// </summary>
    /// <param name="ar">Ar.</param>
    private void ServerReceiveCallback(System.IAsyncResult ar)
    {
        //接收套接字
        Socket workingSocket = ar.AsyncState as Socket;
        //结束接收消息
        int count = workingSocket.EndReceive(ar);
        //将消息取出
        string currentMsg = UTF8Encoding.UTF8.GetString(serverBuffer);
        //清空缓存
        serverBuffer = new byte[100];
        //将接收到的消息传到上层
        serverCallback("ReveiveMsg From " + workingSocket.LocalEndPoint + " Data:" + currentMsg);
        //尾递归
        //5、开启异步接收客户端的消息
        workingSocket.BeginReceive(serverBuffer/*消息缓存*/, 0/*消息起始点*/, serverBuffer.Length/*消息长度*/,
            SocketFlags.None/*消息标志位*/, ServerReceiveCallback/*消息回调*/, workingSocket);
    }
    /// <summary>
    /// 服务器发送消息方法
    /// </summary>
    /// <param name="msg">Message.</param>
    public void ServerSend(string msg)
    {
        //将要发送的消息转成比特流
        serverBuffer = UTF8Encoding.UTF8.GetBytes(msg);
        //异步发送
        m_workingSocket.BeginSend(serverBuffer, 0, serverBuffer.Length, SocketFlags.None, ServerSendCallback, m_workingSocket);
        //将发送消息的信息发送上层
        serverCallback("Server Send Msg : " + msg);
    }

    /// <summary>
    /// 服务器异步发送的回调
    /// </summary>
    /// <param name="ar">Ar.</param>
    private void ServerSendCallback(System.IAsyncResult ar)
    {
        Socket workingSocket = ar.AsyncState as Socket;
        //服务器结束发送消息
        int count = workingSocket.EndSend(ar);
    }

    #endregion

    #region 客户端

    //客户端套接字
    private Socket clientSocket;
    //客户端回调
    private SocketCallback clientCallback;
    //客户端缓存
    private byte[] clientBuffer;

    public void ClientConnect(string serverIp, int serverPort, SocketCallback cb)
    {
        
        //注册客户端回调
        clientCallback = cb;
        //初始化缓存
        clientBuffer = new byte[100];
        //1、初始化客户端Socket
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //2、连接服务器
        clientSocket.BeginConnect(new IPEndPoint(IPAddress.Parse(serverIp), serverPort)/*服务器的网络节点*/,
            ClientConnectCallback, clientSocket);

    }
    /// <summary>
    /// 客户端连接的回调
    /// </summary>
    /// <param name="ar">Ar.</param>
    private void ClientConnectCallback(System.IAsyncResult ar)
    {
        //连接好的套接字
        Socket connectSocket = ar.AsyncState as Socket;
        //向上层发送连接成功的消息
        clientCallback("Has Connect To Server : " + connectSocket.LocalEndPoint);
        //结束连接
        connectSocket.EndConnect(ar);
        //3、开启异步接收消息
        connectSocket.BeginReceive(clientBuffer, 0, clientBuffer.Length, SocketFlags.None, ClientReceiveCallback, connectSocket);
    }
    /// <summary>
    /// 客户端异步接收消息的回调
    /// </summary>
    /// <param name="ar">Ar.</param>
    private void ClientReceiveCallback(System.IAsyncResult ar)
    {
        Socket workingSocket = ar.AsyncState as Socket;
        //接收完毕
        int count = workingSocket.EndReceive(ar);
        //取出消息
        string data = UTF8Encoding.UTF8.GetString(clientBuffer);
        //接收到消息
        clientCallback("ReceiveMsg : " + data);
        //清空
        clientBuffer = new byte[100];
        //尾递归
        //3、开启异步接收消息
        workingSocket.BeginReceive(clientBuffer, 0, clientBuffer.Length, SocketFlags.None, ClientReceiveCallback, workingSocket);
    }

    /// <summary>
    /// 客户端发送方法
    /// </summary>
    /// <param name="msg">Message.</param>
    public void ClientSend(string msg)
    {
        //将字符串转成比特流
        clientBuffer = UTF8Encoding.UTF8.GetBytes(msg);
        //4、异步发送
        clientSocket.BeginSend(clientBuffer, 0, clientBuffer.Length, SocketFlags.None, ClientSendCallback, clientSocket);
    }
    /// <summary>
    /// 客户端发送回调
    /// </summary>
    /// <param name="ar">Ar.</param>
    private void ClientSendCallback(System.IAsyncResult ar)
    {
        Socket workingSocket = ar.AsyncState as Socket;
        //结束发送
        workingSocket.EndSend(ar);
    }
    #endregion


}
