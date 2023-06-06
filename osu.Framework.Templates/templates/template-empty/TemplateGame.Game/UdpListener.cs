using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using osuTK;

namespace UdpTest.Game;

public class UdpListener
{
    public UdpListener(bool isClient, string ip)
    {
        if (isClient)
        {
            this.client = new UdpClient();
            this.serverIp = new IPEndPoint(IPAddress.Parse(ip), 1235); //adresa na kterou se bude posilat
            this.server = new UdpClient(1236); //adresa serveru
            this.remoteIp = new IPEndPoint(IPAddress.Any, 0);
        }
        else
        {
            this.client = new UdpClient();
            this.serverIp = new IPEndPoint(IPAddress.Parse(ip), 1236); //adresa na kterou se bude posilat
            this.server = new UdpClient(1235); //adresa serveru
            this.remoteIp = new IPEndPoint(IPAddress.Any, 0);
        }
    }

    UdpClient server;
    IPEndPoint remoteIp;
    UdpClient client;
    IPEndPoint serverIp;
    string[] cords = new string[1];
    string message = "";
    public bool WaitingForDestroy = false;

    public string Listen()
    {
        while (WaitingForDestroy == false)
        {
            try
            {
                byte[] data = server.Receive(ref remoteIp);
                server.Send(data, data.Length, remoteIp);
                string message = Encoding.ASCII.GetString(data);
                Console.WriteLine(message);
                return message;
            }
            catch (Exception e)
            {
                //Close();
                Console.WriteLine(e.ToString());
            }
        }

        return "";
    }

    public void Send(Vector2 playerPos, Vector2 ballPos, bool moving, string scoreText)
    {
        position cords1 = cordsInput(playerPos.X, playerPos.Y);
        position cords2 = cordsInput(ballPos.X, ballPos.Y);
        string message = cords1.x + "," + cords1.y + "," + cords2.x + "," + cords2.y + "," + moving + "," + scoreText;
        byte[] data = Encoding.ASCII.GetBytes(message);
        client.Send(data, data.Length, serverIp);
        Console.WriteLine($"Sent message: {message}");
    }

    position cordsInput(float posX, float posY)
    {
        int x = (int)posX;
        int y = (int)posY;
        position cords = new position();
        cords.x = x;
        cords.y = y;
        return cords;
    }

    public string[] HandShake(bool isClient)
    {
        string HandshakeMessage = "";
        string[] HandshakeArray = new string[1];
        byte[] HandshakeData;

        if (isClient)
        {
            HandshakeMessage = Listen();
            HandshakeData = Encoding.ASCII.GetBytes(GameSettings.ToString());//Sends Clients Settings
            client.Send(HandshakeData, HandshakeData.Length, serverIp);
        }

        else
        {
            bool success = false;

            while (success == false)
            {
                try
                {
                    HandshakeMessage = GameSettings.ToString();
                    HandshakeData = Encoding.ASCII.GetBytes(HandshakeMessage);
                    client.Send(HandshakeData, HandshakeData.Length, serverIp);

                    if (server.Available > 0) success = true;
                    TcpClient tcpClient = new TcpClient("127.0.0.1", 1267);
                    Thread.Sleep(32);
                }
                catch (Exception e)
                {
                }
            }

            HandshakeMessage = Listen();
        }

        HandshakeArray = HandshakeMessage.Split(';');
        return HandshakeArray;
    }

    public string[] Networking(Vector2 playerPos, Vector2 ballPos, bool moving, string scoreText)
    {
        while (true)
        {
            try
            {
                Send(playerPos, ballPos, moving, scoreText);
                message = Listen();
            }
            catch (Exception e)
            {
            }

            cords = message.Split(',');
            return cords;
        }
    }

    public void Close()
    {
        server.Close();
        client.Close();
        WaitingForDestroy = true;
    }
}

struct position
{
    public int x;
    public int y;
}
