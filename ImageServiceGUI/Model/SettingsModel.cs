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
        private List<string> listOfDir;
        public event PropertyChangedEventHandler PropertyChanged;
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        static TcpClient client = new TcpClient();
        static NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;



        public SettingsModel()
        {
            client.Connect(ep);
            stream = client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);

        }


        public void RemoveHandler(string name)
        {
            
            {
                writer.Write(name);
            }
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
                string toBreak = "";
            // Send data to server
            //sending app config.....
            JObject obj2 = new JObject();
            obj2["inst"] = "1";
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
                    this.lbHandlers.Add(item);
                }



            
        }




        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            JObject jobj = new JObject();
            jobj["dir"] = prop;
             


            client.Connect(ep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.Write(jobj.ToString());



            }
        }
       
    }
}
