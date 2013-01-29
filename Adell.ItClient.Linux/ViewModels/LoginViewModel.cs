using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Windows.Forms;
using Adell.Convenience.Extensions;
using Adell.ItClient.Common.ServiceDefinition;
using Adell.ItClient.Common.Services;
using Adell.ItClient.Linux.Interfaces;
using Adell.ItClient.Linux.Properties;
using Adell.ItClient.Linux.Services;

namespace Adell.ItClient.Linux.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly DelegateCommand<string> _loginCommand;
        private bool _canLogin = true;
        private string _status;
        private readonly CardAuthenticationService _cardService;
        private ILoginService _loginService;
     

        public LoginViewModel(IDispatcher dispatcher) 
        {
            Status = "Ready";
        
            Hosts = new BindingList<string>();
            _loginService = new LinuxLoginService(dispatcher);
            
            //var canService = new CanDiscoveryService("_smb._tcp", Settings.Default.MDnsDomain);
            var canService = new CanDiscoveryService();
            canService
                .Cans
                .ObserveOn(WindowsFormsSynchronizationContext.Current)
                .Subscribe(UpdateCans);

            //var insertion = Observable.Timer(TimeSpan.FromSeconds(5))
            //    .Select(_ => Tuple.Create(100u, Observable.Never<Unit>()));

            // insertion
            //.ObserveOn(WindowsFormsSynchronizationContext.Current)
                
            _cardService = new CardAuthenticationService();
            var subscription =
                _cardService.Insertion
                    .Subscribe(
                        insert =>
                            {
                                Debug.WriteLine("LoginViewModel: Card inserted");
                                _loginService.Connect(
                                    Settings.Default.CardHost,
                                    insert.Item1.ToString(),
                                    String.Empty, 
                                    true);

                                insert.Item2.Subscribe(_ => _loginService.Disconnect());
                            },
                            error => dispatcher.Dispatch(() => Status = error.Message));

            Application.ApplicationExit += (s, e) => subscription.Dispose();
         
            _loginCommand = new DelegateCommand<string>(
                _ =>  _loginService.Connect(Hostname, Username, Password),
                _ => CanLogin);

            _loginService.StateChanged
                .ObserveOn(WindowsFormsSynchronizationContext.Current)
                .Subscribe(
                state =>
                    {
                        CanLogin = state.Status == LoginStatus.Disconnected;
                        Status = state.Status.ToString();
                        if (state.Error != null)
                            MessageBox.Show(state.Error.Message + Environment.NewLine + state.Error.StackTrace);
                    });
        }

        private void UpdateCans(IEnumerable<Common.Models.Can> cans)
        {
            Hosts.Clear();
            cans.Run(can => Hosts.Add(can.HostName));
            if (!String.IsNullOrEmpty(Settings.Default.LastHost))
                Hosts.Add(Settings.Default.LastHost);
        }

        public BindingList<string> Hosts { get; private set; }

    

        public string Username
        {
            get { return Settings.Default.LastUserLogon; }
            set
            {
                if (Settings.Default.LastUserLogon != value)
                {
                    Settings.Default.LastUserLogon = value;
                    RaisePropertyChanged("Username");
                }
            }
        }

        public string Hostname
        {
            get { return Settings.Default.LastHost; }
            set
            {
                if (Settings.Default.LastHost != value)
                {
                    Settings.Default.LastHost = value;
                    RaisePropertyChanged("Hostname");
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }


        public IFormsCommand LoginCommand
        {
            get { return _loginCommand; }
        }

        public bool CanLogin
        {
            get { return _canLogin; }
            private set
            {
                _canLogin = value;
                _loginCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("CanLogin");
            }
        }

        public string Status
        {
            get { return _status; }
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }
    }
}