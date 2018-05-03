using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{



    class SettingsModel : ISettingsModel
    {
        private string outputDirectory;
        private string source;
        private string log;
        private int thumbSize;
        public event PropertyChangedEventHandler PropertyChanged;


        public string outputDir {
            set
            {
                this.outputDir = value;
                this.NotifyPropertyChanged("OutputDirectory");
            }
            get { return this.outputDir; }
        }


        public string Source
        {
            set
            {
                this.source = value;
                this.NotifyPropertyChanged("source");
            }
            get { return this.source; }
        }


        public string Log
        {
            set
            {
                this.log = value;
                this.NotifyPropertyChanged("log");
            }
            get { return this.log; }
        }


        public int ThumbSize
        {
            set
            {
                this.thumbSize = value;
                this.NotifyPropertyChanged("source");
            }
            get { return this.thumbSize; }
        }


        public void GetSettingsFromService()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write("AppConfig");
                int num = int.Parse(Console.ReadLine());
                writer.Write(num);



                // Get result from server
                this.thumbSize = reader.ReadInt32();
                this.source = reader.ReadString();
                this.log = reader.ReadString();
                this.outputDirectory = reader.ReadString();
            }
            client.Close();
        }


        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }




    }
}
