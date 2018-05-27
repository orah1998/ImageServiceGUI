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
    class VMlog : IVMlog
    {
        private LogData selectedItem;
        private LogModel model;
        private int flag = 1;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<LogData> VM_logsList = new ObservableCollection<LogData>();

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


        


        public ObservableCollection<LogData> VM_LogsList
        {
            get { return this.model.LogsList; }
        }






        public void NotifyPropertyChanged(string prop)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }
}
