﻿using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input; 

namespace ImageServiceGUI.ViewModel
{
    class VMSettings : IVMSettings
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ISettingsModel model;
        private ObservableCollection<string> lbHandlers;

        public VMSettings(ISettingsModel model)
        {
            this.model = model;
            this.model.GetSettingsFromService();
            model.PropertyChanged+= delegate (Object sender, PropertyChangedEventArgs e)
            {
                Console.WriteLine(e.PropertyName);
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            lbHandlers.Add("yair");
            lbHandlers.Add("or");
        }


        public string VM_OutputDirectory
        {
            get { return this.model.outputDir; }
        }

        public string VM_SourceName
        {
            get {return this.model.Source; }
        }

        public string VM_LogName
        {
            get { return this.model.Log; }
        }

        public int VM_ThumbnailSize
        {
            get { return this.model.ThumbSize; }
        }

        public ObservableCollection<string> LbHandlers
        {
            get { return this.lbHandlers; }
        }


       public void NotifyPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


    }
}
