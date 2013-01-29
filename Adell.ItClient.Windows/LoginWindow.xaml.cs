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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Adell.ItClient.Common.Services;
using Adell.ItClient.Windows.Helpers;
using Adell.ItClient.Windows.Properties;
using Adell.ItClient.Windows.Services;

namespace Adell.ItClient.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //TODO: IoC or ServiceLocator
                var canDiscovery = new CanDiscoveryService();
                //var canDiscovery = new CanDiscoveryService("_smb._tcp", Settings.Default.MDnsDomain);
                var loginService = new WindowsLoginService();
                DataContext = new ViewModels.LoginViewModel(loginService, canDiscovery);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
                App.Current.Shutdown();
            }
        }
    }
}
