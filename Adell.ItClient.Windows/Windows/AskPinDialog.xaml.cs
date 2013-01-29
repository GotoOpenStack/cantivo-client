using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Adell.ItClient.Windows.Windows
{
    /// <summary>
    /// Interaction logic for AskPinDialog.xaml
    /// </summary>
    public partial class AskPinDialog : Window
    {
        public AskPinDialog()
        {
            InitializeComponent();
        }

        void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            // Accept the dialog and return the dialog result
            this.DialogResult = true;
        }
    }
}
