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
        private ObservableCollection<string> lbHandlers = new ObservableCollection<string>();
        private string selectedItem;

        //we will use remove as a delegate to remove handlers!
        public ICommand Remove { get; private set; }

        public VMSettings()
        {
            
                model = new SettingsModel();
                this.model.GetSettingsFromService();
                //   this.model.GetSettingsFromService();
                model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
                 {
                     Console.WriteLine(e.PropertyName);
                     NotifyPropertyChanged("VM_" + e.PropertyName);
                 };
        }

        public void update(object obj)
        {
            // send to server remove from handlers
            if (this.CanExecuteRemove(obj))
            {
                this.model.RemoveHandler(this.selectedItem);
                this.model.LbHandlers.Remove(SelectedItem);

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
                using (StreamWriter outputFile = File.AppendText(@"C:\Users\yaire\Desktop\GUI.txt"))
                {
                    outputFile.WriteLine(value);
                }
                this.selectedItem = value;
                this.NotifyPropertyChanged(value);
            }
            get { return this.selectedItem; }
        }
         

        

        public void NotifyPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            //making the button pressable
            //var command = this.Remove as RemoveCommand<object>;
//            command.RaiseCanExecuteChange();

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
