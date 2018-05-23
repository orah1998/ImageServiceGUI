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
        private string outputDirectory;
        private string source;
        private string log;
        private int thumbSize;
        private ObservableCollection<string> lbHandlers = new ObservableCollection<string>();
        private List<string> listOfDir;
        public event PropertyChangedEventHandler PropertyChanged;
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8200);
        TcpClient client = new TcpClient();


        public ObservableCollection<string> LbHandlers
        {
            get
            {
                return this.lbHandlers;
            }
        }


        public string outputDir
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
            string toBreak = "";
            client.Connect(ep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                writer.Write("AppConfig");


                // Get result from server
                string ans = reader.ReadString();
                // Handler" value="C: \Users\Operu\Desktop\mip"/>
                //"OutputDir" value = "C:\Users\Operu\Desktop\dest" />
                //"SourceName" value = "ImageServiceSource" />
                //"LogName" value = "ImageServiceLog" />
                // "ThumbnailSize"
                JObject obj = JsonConvert.DeserializeObject<JObject>(ans);
                this.outputDir = obj["OutputDir"].ToString();
                this.thumbSize = int.Parse(obj["ThumbnailSize"].ToString());
                this.source = obj["SourceName"].ToString();
                this.log = obj["LogName"].ToString();
                toBreak = obj["Handler"].ToString();
            }
            foreach (string item in toBreak.Split(';')){
                this.lbHandlers.Add(item);
            }

            client.Close();

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
