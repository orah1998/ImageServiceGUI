using System;
using System.Net;
using System.Net.Sockets;

public class SingletonClient
{
    private static SingletonClient instance = null;

    private TcpClient client;

    /// <summary>
    /// singeltoon cliieeent
    /// </summary>
    public SingletonClient()

    {
        
    }

    /// <summary>
    /// iinstacne
    /// </summary>
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
    /// <summary>
    /// connect new
    /// </summary>
    public void Connect()
    {
        try
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        client = new TcpClient();

        client.Connect(ep);

        }catch(Exception e)
        {
            
        }
    }

    /// <summary>
    /// get
    /// </summary>
    /// <returns></returns>
    public TcpClient getClient()
    {
        return this.client;
    }
    /// <summary>
    /// cloose
    /// </summary>
    public void Closing()
    {
        client.Close();
    }



}
