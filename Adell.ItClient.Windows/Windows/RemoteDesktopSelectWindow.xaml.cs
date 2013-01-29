using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Windows.ViewModels;

namespace Adell.ItClient.Windows.Windows
{
    /// <summary>
    /// Interaction logic for RemoteDesktopSelectWindow.xaml
    /// </summary>
    public partial class RemoteDesktopSelectWindow : Window
    {
        public RemoteDesktopSelectWindow(RemoteDesktopSelectViewModel viewModel)
        {
            InitializeComponent();
           DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
