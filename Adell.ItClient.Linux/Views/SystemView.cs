using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Adell.ItClient.Linux.ViewModels;
using Adell.ItClient.Linux.Extensions;

namespace Adell.ItClient.Linux.Views
{
    public partial class SystemView : UserControl
    {
        private readonly SystemViewModel _viewModel;

        public SystemView()
        {
            InitializeComponent();
            _viewModel = new SystemViewModel();
            buttonShutDown.BindCommand(_viewModel.ShutdownCommand);
            buttonReboot.BindCommand(_viewModel.RebootCommand);
            Visible = _viewModel.CanShutdown;
        }
    }
}
