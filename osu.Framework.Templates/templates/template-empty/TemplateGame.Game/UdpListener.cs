using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using osuTK;

namespace UdpTest.Game;

public class UdpListener
{
    public UdpListener(bool isClient)
    {
        if (isClient)
        {
            this.client = new UdpClient();
            this.serverIp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1235); //adresa na kterou se bude posilat
            this.server = new UdpClient(1236); //adresa serveru
            this.remoteIp = new IPEndPoint(IPAddress.Any, 0);
        }
        else
        {
            this.client = new UdpClient();
            this.serverIp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1236); //adresa na kterou se bude posilat
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

    public string Listen()
    {
        while (true)
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
                Console.WriteLine(e.ToString());
            }
        }
    }

    public void Send(Vector2 playerPos, Vector2 ballPos, bool moving)
    {
        position cords1 = cordsInput(playerPos.X, playerPos.Y);
        position cords2 = cordsInput(ballPos.X, ballPos.Y);
        string message = cords1.x + "," + cords1.y + "," + cords2.x + "," + cords2.y + "," + moving;
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

    public string[] Networking(Vector2 playerPos, Vector2 ballPos, bool moving)
    {
        while (true)
        {
            try
            {
                Send(playerPos, ballPos, moving);
                message = Listen();
            }
            catch (Exception e)
            {
            }

            cords = message.Split(',');
            return cords;
        }
    }
}

struct position
{
    public int x;
    public int y;
}
