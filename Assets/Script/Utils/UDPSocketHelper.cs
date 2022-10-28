using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPSocketHelper : MonoBehaviour
{
    private UdpClient client;
    private Thread thread = null;
    private IPEndPoint remotePoint;
    public string ip = "127.0.0.1";
    public int port = 9090;

    public Action<string, string> receiveMsg = null;

    private string receiveString = null;

    // Use this for initialization
    void Start()
    {
        //ip = IPManager.ipAddress;
        remotePoint = new IPEndPoint(IPAddress.Any, 0);
        thread = Loom.RunAsync(ReceiveMsg);
    }
    //接受消息
    void ReceiveMsg()
    {
        while (true)
        {
            client = new UdpClient(port);
            byte[] receiveData = client.Receive(ref remotePoint);//接收数据
            receiveString = Encoding.UTF8.GetString(receiveData);
            if (!string.IsNullOrEmpty(receiveString))
            {
                Loom.QueueOnMainThread(() =>
                {
                    var remoteIP = remotePoint.Address.ToString();
                    if (receiveMsg != null && remoteIP != ip)
                    {
                        // Debug.Log(remotePoint.Address + ":" + remotePoint.Port + " ---> " + receiveString);
                        receiveMsg.Invoke(remoteIP, receiveString);
                        receiveString = null;
                    }
                });
            }
            client.Close();
        }
    }
    //发送消息
    public void SendMsg(string ip, int port, string _msg)
    {
        Loom.RunAsync(() =>
        {
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse(ip), port);
            if (_msg != null)
            {
                byte[] sendData = Encoding.Default.GetBytes(_msg);
                UdpClient client = new UdpClient();
                client.Send(sendData, sendData.Length, remotePoint);
                client.Close();
            }
        });
    }

    void OnDestroy()
    {
        SocketQuit();
    }
    void SocketQuit()
    {
        if (thread != null)
        {
            thread.Abort();
            thread.Interrupt();
            thread = null;
        }
        if (client != null)
        {
            client.Close();
            client = null;
        }
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}