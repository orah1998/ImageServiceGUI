using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface ISettingsModel : INotifyPropertyChanged
    {

        string outputDir { set; get; }
        string Source { set; get; }
        string Log { set; get; }
        int ThumbSize { set; get; }
        void GetSettingsFromService();

    }
}
