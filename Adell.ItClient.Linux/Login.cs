using System;
using System.Windows.Forms;
using Adell.ItClient.Linux.Services;
using Adell.ItClient.Linux.ViewModels;
using Adell.ItClient.Linux.Extensions;

namespace Adell.ItClient.Linux
{
    public partial class Login : Form
    {
        private readonly LoginViewModel _loginViewModel;

        public Login()
        {
            InitializeComponent();
            _loginViewModel = new LoginViewModel(new FormsDispatcher(this));
            comboBox1.DataSource = _loginViewModel.Hosts;
            username.DataBindings.Add("Text", _loginViewModel, "Username");
            password.DataBindings.Add("Text", _loginViewModel, "Password");
            comboBox1.DataBindings.Add("Text", _loginViewModel, "Hostname");
            info.DataBindings.Add("Text", _loginViewModel, "Status");
            button1.BindCommand(_loginViewModel.LoginCommand);
        }

      

        private void SelectNext(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == 13)
            {
                if (!String.IsNullOrEmpty(_loginViewModel.Hostname) &&
                    !String.IsNullOrEmpty(_loginViewModel.Password) &&
                    !String.IsNullOrEmpty(_loginViewModel.Username))
                {
                    _loginViewModel.LoginCommand.Execute(null);
                }
                else
                {
                    SelectNextControl((Control) sender, true, true, false, false);
                }
            }
             */
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return && (ActiveControl is TextBox || ActiveControl is ComboBox))
                return SelectNextControl(ActiveControl, true, true, true, true);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ActiveControl = ((username.Text == String.Empty) ? username : password);
        }       
    }
}