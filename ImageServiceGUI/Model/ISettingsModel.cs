using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface ISettingsModel : INotifyPropertyChanged
    {

        string OutputDir { set; get; }
        string Source { set; get; }
        string Log { set; get; }
        int ThumbSize { set; get; }
        void GetSettingsFromService();
        ObservableCollection<string> LbHandlers { get; }

        void RemoveHandler(string selectedItem);
    }
}
