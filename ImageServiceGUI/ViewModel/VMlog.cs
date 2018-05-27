using ImageServiceGUI.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    /// <summary>
    /// view model of log
    /// </summary>
    class VMlog : IVMlog
    {
        private LogData selectedItem;
        private LogModel model;
        private int flag = 1;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<LogData> VM_logsList = new ObservableCollection<LogData>();
        /// <summary>
        /// cnstrctr
        /// </summary>
        public VMlog()
        {
            model = new LogModel();
            this.model.GetLogHistoryFromService();
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
              {
                  using (StreamWriter sw = File.AppendText(@"C: \Users\Operu\Desktop\testing\info.txt"))
                  {
                      sw.WriteLine(e.PropertyName);
                  }
                  NotifyPropertyChanged("VM_" + e.PropertyName);
              };
            this.model.GetLogFromService();

        }
        /// <summary>
        /// view model list
        /// </summary>
        public ObservableCollection<LogData> VM_LogsList
        {
            get { return this.model.LogsList; }
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
