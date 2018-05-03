using ImageServiceGUI.Model;
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
        public ICommand RemoveCommand { get; private set; }


        public VMSettings(ISettingsModel model)
        {
            this.model = model;
            model.PropertyChanged+= delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        private void NotifyPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChange();
        }

        public string VM_OutputDirectory
        {
            get { return this.model.OutputDirectory; }
        }

        public string VM_SourceName => throw new NotImplementedException();

        public string VM_LogName => throw new NotImplementedException();

        public int VM_ThumbnailSize => throw new NotImplementedException();

        public ObservableCollection<string> LbHandlers => throw new NotImplementedException();

        
    }
}
