using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        private string selectedItem;
        private bool connection;
        //we will use remove as a delegate to remove handlers!
        public ICommand Remove { get; private set; }

        public VMSettings()
        {
            
                model = new SettingsModel();
                this.model.GetSettingsFromService();
                model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
                 {
                     NotifyPropertyChanged("VM_" + e.PropertyName);
                 };
            this.model.listenFolders();
        }

        public void update(object obj)
        {
            // send to server remove from handlers
            if (this.CanExecuteRemove(obj))
            {
                //removing the handler from the server
                this.model.RemoveHandler(this.selectedItem);

                //removing the handler from THIS GUI
                this.model.LbHandlers.Remove(SelectedItem);

            }


        }

        public bool VM_Connection
        {
            get
            {
                return this.model.connection;
            }
            set
            {
                this.model.connection = value;
                this.NotifyPropertyChanged("Connection");
            }
        }

        public string VM_OutputDirectory
        {
            get { return this.model.OutputDir; }
        }

        public string VM_SourceName
        {
            get { return this.model.Source; }
        }

        public string VM_LogName
        {
            get { return this.model.Log; }
        }

        public int VM_ThumbnailSize
        {
            get { return this.model.ThumbSize; }
        }

        public ObservableCollection<string> VM_LbHandlers
        {
            get { return this.model.LbHandlers; }
        }



        public string SelectedItem
        {
            set
            {
                this.selectedItem = value;
                this.NotifyPropertyChanged(value);
            }
            get { return this.selectedItem; }
        }
         

        

        public void NotifyPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


        private bool CanExecuteRemove(object parameter)
        {
            if (this.SelectedItem != null)
            {
                return true;
            }
            return false;
        }
    }
}
