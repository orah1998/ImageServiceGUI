using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ImageServiceGUI.ViewModel
{   
    /// <summary>
    /// vm settngs
    /// </summary>
    interface IVMSettings : INotifyPropertyChanged
    {
        string VM_OutputDirectory { get; }
        string VM_SourceName { get; }
        string VM_LogName { get; }
        int VM_ThumbnailSize { get; }
        string SelectedItem { get; set; }
        ObservableCollection<string> VM_LbHandlers { get; }
        void update(object obj);
    }
}
