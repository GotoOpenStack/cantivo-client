using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Adell.ItClient.Windows.Helpers;

namespace Adell.ItClient.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            App.Current.DispatcherUnhandledException +=
                (s, e_) => MessageBox.Show(e_.Exception.Message +e_.Exception.StackTrace);
            base.OnStartup(e);
        }
    }
}
