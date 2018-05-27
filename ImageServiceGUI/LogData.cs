using ImageServiceGUI.ENUMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI
{
    /// <summary>
    /// data oooof typpppee and meessagee
    /// </summary>
    class LogData
    {
        private string logMessage;
        private MessageTypeEnum logType;


        /// <param name="message"></param>
        public LogData(MessageTypeEnum type, string message)
        {
            this.LogMessage = message;
            this.LogType = type;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LogMessage
        {
            get
            {
                return this.logMessage;
            }
            set
            {
                this.logMessage = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public MessageTypeEnum LogType
        {
            get
            {
                return this.logType;
            }
            set
            {
                this.logType = value;
            }
        }
    }
}
