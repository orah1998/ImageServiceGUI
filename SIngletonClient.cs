using System;

public class SingletonClient
{
    private static SingletonClient instance = null;

    private TcpClient client;

    public SIngletonClient()

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
        client.Connect(ep);
    }


    public void Closing()
    {
        client.close();
    }



}
