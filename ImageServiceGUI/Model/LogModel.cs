using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel
    {
        private ObservableCollection<LogData> logsList = new ObservableCollection<LogData>();
        public ObservableCollection<LogData> LogsList
        {
            get { return this.logsList; }
            set
            {
                this.logsList = value;
                this.NotifyPropertyChanged("LogsList");
            }
        }

        private void NotifyPropertyChanged(string v)
        {
            throw new NotImplementedException();
        }
    }
}
