using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    class VMlog : IVMlog
    {
        
        private LogData selectedItem;
        private LogModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LogData> VM_LogsList
        {
            get { return this.model.LogsList; }
        }
    }
}
