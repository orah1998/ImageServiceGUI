using System;
using System.Net;
using System.Net.Sockets;

public class SingletonClient
{
    private static SingletonClient instance = null;

    private TcpClient client;

    public SingletonClient()

    {
        
    }


    public static SingletonClient Instance
    {
        get
        {

            if (instance == null)
            {
                instance = new SingletonClient();
            }
            return instance;

        }
    }

    public void Connect()
    {
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        client = new TcpClient();
        try { 
        client.Connect(ep);
        }catch(Exception e)
        {
            throw new Exception();
        }
    }


    public TcpClient getClient()
    {
        return this.client;
    }

    public void Closing()
    {
        client.Close();
    }



}
