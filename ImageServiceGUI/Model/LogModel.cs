using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel
    {
        private ObservableCollection<LogData> logsList = new ObservableCollection<LogData>();
        private string logType;
        private string messageType;
        BinaryReader reader;
        BinaryWriter writer;


        public ObservableCollection<LogData> LogsList
        {
            get { return this.logsList; }
            set
            {
                this.logsList = value;
            }
        }

        public void GetLogHistoryFromService()
        {
            // ask the server for old log            
            JObject obj = new JObject();
            obj["inst"] = "4";
            string command = JsonConvert.SerializeObject(obj);
            writer.Write(JsonConvert.SerializeObject(obj));

            // getting old log
            string ans = reader.ReadString();
            JObject obj2 = JsonConvert.DeserializeObject<JObject>(ans);


            //model.LogsList.Add(new LogData());
        }

        public void GetLogFromService()
        {
            
            string str = reader.ReadString();
            List<LogData> logData = JsonConvert.DeserializeObject<List<LogData>>(str);
            foreach (LogData log in logData)
            {
                this.LogsList.Add(log);
            }
        }
    }
}
