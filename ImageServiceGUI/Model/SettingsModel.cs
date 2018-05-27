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
using System.Threading;
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
        public ObservableCollection<string> lbHandlers = new ObservableCollection<string>();
        public event PropertyChangedEventHandler PropertyChanged;
        static NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;

        private static Mutex mutex;
        private bool connection;
        /// <summary>
        /// conneection
        /// </summary>
        public bool Connection
        {
            get
            {
                return this.connection;
            }
            set
            {
                this.connection = value;
                this.NotifyPropertyChanged("Connection");

            }
        }

        /// <summary>
        /// coonstrrructor
        /// </summary>
        public SettingsModel()
        {
            mutex = new Mutex();
        }

        /// <summary>
        /// rremooovee foldeerrr
        /// </summary>
        /// <param name="name"></param>
        public void RemoveHandler(string name)
        {
            mutex.WaitOne();
            SingletonClient.Instance.Connect();
            stream=SingletonClient.Instance.getClient().GetStream();

            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);

            JObject obj = new JObject();
            obj["inst"] = "3";
            obj["etc"] = name;

            writer.Write(JsonConvert.SerializeObject(obj));

            SingletonClient.Instance.Closing();
            mutex.ReleaseMutex();
        }




        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> LbHandlers
        {
            get
            { 

                return this.lbHandlers;
            }
        }

        /// <summary>
        /// outputdir
        /// </summary>
        public string OutputDir
        {
            set
            {
                this.outputDir = value;
                this.NotifyPropertyChanged("OutputDirectory");
            }
            get { return this.outputDir; }
        }

        /// <summary>
        /// source
        /// </summary>
        public string Source
        {
            set
            {
                this.source = value;
                this.NotifyPropertyChanged("source");
            }
            get { return this.source; }
        }

        /// <summary>
        /// log
        /// </summary>
        public string Log
        {
            set
            {
                this.log = value;
                this.NotifyPropertyChanged("log");
            }
            get { return this.log; }
        }

        /// <summary>
        /// size
        /// </summary>
        public int ThumbSize
        {
            set
            {
                this.thumbSize = value;
                this.NotifyPropertyChanged("ThumbnailSize");
            }
            get { return this.thumbSize; }
        }

        /// <summary>
        /// seettiings
        /// </summary>
        public void GetSettingsFromService()
        {
            ObservableCollection<string> temp = new ObservableCollection<string>();
            mutex.WaitOne();
            try
            {
                this.connection = true;
                SingletonClient.Instance.Connect();
            stream = SingletonClient.Instance.getClient().GetStream();

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
            SingletonClient.Instance.Closing();
            mutex.ReleaseMutex();


            }
            catch (Exception e)
            {
                this.connection = false;
            }


        }
        /// <summary>
        /// listeening all timee
        /// </summary>
        public void listenFolders()
        {
            ObservableCollection<string> temp = new ObservableCollection<string>();
            new Task(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(2000);
                    temp = new ObservableCollection<string>();
                    try
                        {
                        mutex.WaitOne();
                        
                            SingletonClient.Instance.Connect();
                            this.connection = true;
                        
                        stream = SingletonClient.Instance.getClient().GetStream();

                        reader = new BinaryReader(stream);
                        writer = new BinaryWriter(stream);
                        JObject obj2 = new JObject();
                        obj2["inst"] = "6";
                        obj2["etc"] = "1";
                        writer.Write(JsonConvert.SerializeObject(obj2));
                        string ret=reader.ReadString();
                        JObject objRet= JsonConvert.DeserializeObject<JObject>(ret);
                        string toBreak=objRet["Handler"].ToString();
                         foreach (string item in toBreak.Split(';'))
                        {
                            temp.Add(item);
                        }
                        this.lbHandlers = temp;
                        this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("LbHandlers"));
                        SingletonClient.Instance.Closing();
                        mutex.ReleaseMutex();


                    }
                    
                    catch (Exception ex)
                    {
                        this.connection = false;
                    }
                }
            }).Start();

        }

        /// <summary>
        /// uppdAte
        /// </summary>
        /// <param name="prop"></param>
        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
       
    }
}
