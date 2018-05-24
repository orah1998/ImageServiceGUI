using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    interface IVMlog
    {
        ObservableCollection<LogData> VM_LogsList { get; }
    }
}
