﻿using Newtonsoft.Json;
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
    /// moodel oof loog
    /// </summary>
    class LogModel : ILogModel
    {
        private static Mutex mutex;
        public ObservableCollection<LogData> logsList = new ObservableCollection<LogData>();
        private string logType;
        private string messageType;
        BinaryReader reader;
        BinaryWriter writer;
        IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        static TcpClient client = new TcpClient();
        static NetworkStream stream;
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// cooonstctr
        /// </summary>
        public LogModel()
        {
            mutex = new Mutex();
        }

        /// <summary>
        /// list
        /// </summary>
        public ObservableCollection<LogData> LogsList
        {
            get { return this.logsList; }
            set
            {
                this.logsList = value;
                this.NotifyPropertyChanged("LogsList");
            }
        }
        /// <summary>
        /// old log
        /// </summary>
        public void GetLogHistoryFromService()
        {
            try
            {
                mutex.WaitOne();
                SingletonClient.Instance.Connect();
                stream = SingletonClient.Instance.getClient().GetStream();
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);


                // ask the server for old log            
                JObject obj = new JObject();
                obj["inst"] = "2";
                string command = JsonConvert.SerializeObject(obj);
                writer.Write(command);

                // getting old log
                string ans = reader.ReadString();
                JObject obj2 = JsonConvert.DeserializeObject<JObject>(ans);
                string temp = obj2["2"].ToString();

                foreach (string item in temp.Split('|'))
                {
                    ENUMS.MessageTypeEnum typ;
                    if (item.Split(';')[0] == "INFO" || item.Split(';')[0] == "Information")
                    {
                        typ = ENUMS.MessageTypeEnum.INFO;
                        LogData logdata = new LogData(typ, item.Split(';')[1]);
                        this.logsList.Add(logdata);
                    }

                    if (item.Split(';')[0] == "WARNING")
                    {
                        typ = ENUMS.MessageTypeEnum.WARNING;
                        LogData logdata = new LogData(typ, item.Split(';')[1]);
                        this.logsList.Add(logdata);
                    }

                    if (item.Split(';')[0] == "FAIL")
                    {
                        typ = ENUMS.MessageTypeEnum.FAIL;
                        LogData logdata = new LogData(typ, item.Split(';')[1]);
                        this.logsList.Add(logdata);
                    }
                }


                SingletonClient.Instance.Closing();
                mutex.ReleaseMutex();
            }catch(Exception e)
            {

            }

        }

        
        /// <summary>
        /// get neew loog
        /// </summary>
        public void GetLogFromService()
        {
            ObservableCollection<LogData> temp = new ObservableCollection<LogData>(); ;
            new Task(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1500);
                    temp = new ObservableCollection<LogData>();
                    try
                    { 
                        mutex.WaitOne();
                        SingletonClient.Instance.Connect();
                        stream = SingletonClient.Instance.getClient().GetStream();

                        reader = new BinaryReader(stream);
                        writer = new BinaryWriter(stream);
                        JObject obj2 = new JObject();
                        obj2["inst"] = "2";
                        obj2["etc"] = "1";
                        writer.Write(JsonConvert.SerializeObject(obj2));
                        string ret = reader.ReadString();
                        JObject objRet = JsonConvert.DeserializeObject<JObject>(ret);
                        string toBreak = objRet["2"].ToString();

                        foreach (string item in toBreak.Split('|'))
                        {

                            ENUMS.MessageTypeEnum typ;
                            if (item.Split(';')[0] == "INFO" || item.Split(';')[0] == "Information")
                            {
                                typ = ENUMS.MessageTypeEnum.INFO;
                                LogData logdata = new LogData(typ, item.Split(';')[1]);
                                temp.Add(logdata);
                            }

                            if (item.Split(';')[0] == "WARNING")
                            {
                                typ = ENUMS.MessageTypeEnum.WARNING;
                                LogData logdata = new LogData(typ, item.Split(';')[1]);
                                temp.Add(logdata);
                            }

                            if (item.Split(';')[0] == "FAIL")
                            {
                                typ = ENUMS.MessageTypeEnum.FAIL;
                                LogData logdata = new LogData(typ, item.Split(';')[1]);
                                temp.Add(logdata);
                            }

                            
                        }
                        this.LogsList = temp;
                        SingletonClient.Instance.Closing();
                        mutex.ReleaseMutex();
                    }

                    catch (Exception ex)
                    {
                    }
                }
            }).Start();
        }


        /// <summary>
        /// updater
        /// </summary>
        /// <param name="prop"></param>
        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}
