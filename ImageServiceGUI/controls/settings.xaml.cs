﻿using ImageServiceGUI.Model;
using ImageServiceGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageServiceGUI.controls
{
    /// <summary>
    /// Interaction logic for settings.xaml
    /// </summary>
    public partial class settings : UserControl
    {

        private IVMSettings vmsettings;

        public settings()
        {
            InitializeComponent();
            vmsettings = new VMSettings();
            this.DataContext = this.vmsettings;
        }

        private void outputdir_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void logName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void sourceName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.vmsettings.update(vmsettings.SelectedItem);
            
            removeButton.IsEnabled = false;

        }

        private void lbHandlers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removeButton.IsEnabled = true;
        }

       

        private void lbHandlers_Selected(object sender, RoutedEventArgs e)
        {
            removeButton.IsEnabled = true;
        }
    }
}
