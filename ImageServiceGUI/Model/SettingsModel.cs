using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace ImageServiceGUI.Model
{



    /// <summary>
    /// hello my friends
    /// </summary>


    class SettingsModel : ISettingsModel
    {
        private string outputDir;
        private string source;
        private string log;
        private int thumbSize;
        private ObservableCollection<string> lbHandlers = new ObservableCollection<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        static TcpClient client = new TcpClient();
        static NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;



        public SettingsModel()
        {
        }


        public void RemoveHandler(string name)
        {
            TcpClient client2 = new TcpClient();
            client2.Connect(ep);
            stream = client2.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            JObject obj = new JObject();
            obj["inst"] = "3";
            obj["etc"] = name;

            writer.Write(JsonConvert.SerializeObject(obj));
            client.Close();
        }





        public ObservableCollection<string> LbHandlers
        {
            get
            { 
                return this.lbHandlers;
            }
        }


        public string OutputDir
        {
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


            ObservableCollection<string> temp = new ObservableCollection<string>();
            client.Connect(ep);
            stream = client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);

            string toBreak = "";
            // Send data to server
            //sending app config.....
            JObject obj2 = new JObject();
            obj2["inst"] = "1";
            obj2["etc"] = "1";
            writer.Write(JsonConvert.SerializeObject(obj2));

                    // Get result from server
                    string ans = reader.ReadString();
                    JObject obj = JsonConvert.DeserializeObject<JObject>(ans);
                    this.outputDir = obj["OutputDir"].ToString();
                    this.thumbSize = int.Parse(obj["ThumbnailSize"].ToString());
                    this.source = obj["SourceName"].ToString();
                    this.log = obj["LogName"].ToString();
                    toBreak = obj["Handler"].ToString();


                foreach (string item in toBreak.Split(';'))
                {
                    temp.Add(item);
                }
            this.lbHandlers = temp;

            client.Close();


        }

        public void listenFolders()
        {
            new Task(() =>
            {
                System.Threading.Thread.Sleep(500);
                while (true)
                {
                    try
                    {
                        GetSettingsFromService();
                    }
                    
                    catch (Exception ex)
                    {

                    }
                }
            }).Start();

        }


        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
       
    }
}
