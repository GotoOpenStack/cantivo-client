using System;
using System.Reactive.Linq;
using System.Windows.Forms;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Linux.ViewModels;
using Adell.ItCan.Interop.Services;

namespace Adell.ItClient.Linux.Windows
{
    public partial class RemoteDesktopSelectWindow : Form
    {
        private RemoteDesktopSelectViewModel _viewModel;
        public RemoteDesktopSelectWindow() 
            : this(new RemoteDesktopSelectViewModel(new MockHostListService(3).GetHostListAsync().First()))
        {
        }
        
        public RemoteDesktopSelectWindow(RemoteDesktopSelectViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            listBox1.DataSource = viewModel.Desktops;
            listBox1.DisplayMember = "Name";
            listBox1.SelectedValueChanged += (s, e) => _viewModel.SelectedDesktop = (desktop) listBox1.SelectedValue;
        }

        private void listboxDoubleclick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (e.KeyChar == 27)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

    }
}
