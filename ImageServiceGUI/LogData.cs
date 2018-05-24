using ImageServiceGUI.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI
{
    class LogData
    {
        public string LogMessage { get; set; }
        public MessageTypeEnum LogType { get; set; }
        /// <param name="message"></param>
        public LogData(MessageTypeEnum type, string message)
        {
            this.LogMessage = message;
            this.LogType = type;
        }
    }
}
