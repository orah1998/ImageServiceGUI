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
    /// <summary>
    /// view model settings
    /// </summary>
    class VMSettings : IVMSettings
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ISettingsModel model;
        private string selectedItem;
        private bool connection;
        //we will use remove as a delegate to remove handlers!
        public ICommand Remove { get; private set; }
        /// <summary>
        /// coonstruuctor
        /// </summary>
        public VMSettings()
        {
            
                model = new SettingsModel();
            try { 
                this.model.GetSettingsFromService();
            }
            catch(Exception e)
            {

            }
                model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
                 {
                     NotifyPropertyChanged("VM_" + e.PropertyName);
                 };
            this.model.listenFolders();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// color background
        /// </summary>
        public bool VM_Connection
        {
            get
            {
                return this.model.Connection;
            }
            set
            {
                this.model.Connection = value;
            }
        }
        /// <summary>
        /// dir uuooutput
        /// </summary>
        public string VM_OutputDirectory
        {
            get { return this.model.OutputDir; }
        }

        public string VM_SourceName
        {
            get { return this.model.Source; }
        }
        /// <summary>
        /// log namee
        /// </summary>
        public string VM_LogName
        {
            get { return this.model.Log; }
        }
        /// <summary>
        /// siize
        /// </summary>
        public int VM_ThumbnailSize
        {
            get { return this.model.ThumbSize; }
        }
        /// <summary>
        /// list
        /// </summary>
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
         
        /// <summary>
        /// uupdatee
        /// </summary>
        /// <param name="property"></param>
        public void NotifyPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        /// <summary>
        /// noothing
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
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
