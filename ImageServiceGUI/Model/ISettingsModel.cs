using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    /// <summary>
    /// 
    /// </summary>
    interface ISettingsModel : INotifyPropertyChanged
    {

        string OutputDir { set; get; }
        string Source { set; get; }
        string Log { set; get; }
        int ThumbSize { set; get; }
        void GetSettingsFromService();
        ObservableCollection<string> LbHandlers { get; }
        bool Connection { get; set; }

        void RemoveHandler(string selectedItem);
        void listenFolders();
    }
}
