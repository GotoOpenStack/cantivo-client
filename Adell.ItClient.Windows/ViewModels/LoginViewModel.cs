using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Adell.Convenience.Extensions;
using Adell.ItClient.Common.Models;
using Adell.ItClient.Common.ServiceDefinition;
using Adell.ItClient.Common.Services;
using Adell.ItClient.Windows.Properties;

namespace Adell.ItClient.Windows.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        /* 
         * TODO: loginCommand should not depend on the control passwordbox,
         * but its property SecurePassword there was some issues with 
         * element-binding in xaml always returning what I think was the same object: ""
         */
        private readonly DelegateCommand<PasswordBox> _loginCommand;
        private string _status;
        private readonly ICanDiscoveryService _canDiscovery;
        private readonly ILoginService _loginService;
        private bool _canLogin = true;
        private string _password;
        private string _statusMessage;
        private readonly DelegateCommand<object> _showSettingsCommand;


        public LoginViewModel(
            ILoginService loginService, 
            ICanDiscoveryService canDiscoveryService)
        {
            _loginService = loginService;
            _canDiscovery = canDiscoveryService;

             Cans = new ObservableCollection<Can>();
            _loginCommand = new DelegateCommand<PasswordBox>(
                pbx => _loginService.Connect(SelectedCan.HostName, Username, pbx.SecurePassword.String()),
                _ => CanLogin);

            _loginService.StateChanged
               .ObserveOn(SynchronizationContext.Current)
               .Subscribe(
               state =>
               {
                   _canLogin = state.Status == LoginStatus.Disconnected;
                   _loginCommand.RaiseCanExecuteChanged();
                   Status = state.Status.ToString();
                   StatusMessage = (state.Error == null) ? String.Empty : state.Error.Message;
               });
           
            _showSettingsCommand = new DelegateCommand<object>(
                _ => new Windows.SettingsWindow().ShowDialog());

            
            StatusMessage = "Ready";
           
            var settingsChanging = Observable.FromEventPattern<CancelEventArgs>(Settings.Default, "SettingsSaving");
            var customCans = Observable.Defer(() =>
                 (from evt in settingsChanging
                 let custom = Settings.Default.CustomHosts
                 where custom != null
                 select custom)
                    .StartWith(Settings.Default.CustomHosts ?? Enumerable.Empty<Can>()));

            _canDiscovery
               .Cans.StartWith(Enumerable.Empty<Can>())
               .CombineLatest(customCans, (a,b) => a.Concat(b))
               .ObserveOn(SynchronizationContext.Current)
               .Do(_ => { }, ex => StatusMessage = "Zeroconf failed")
               .OnErrorResumeNext(customCans)
               .Subscribe(UpdateCans, ex => StatusMessage = "Cans failure");

            
            var _cardService = new CardAuthenticationService();
            var subscription =
                _cardService.Insertion
                    .ObserveOn(SynchronizationContext.Current)
                   // .Do(_ => { }, error => StatusMessage = error.Message)
                   // .Retry()
                    .Subscribe(
                        insert =>
                            {
                                Debug.WriteLine("LoginViewModel: Card inserted");
                                _loginService.Connect(
                                    Settings.Default.CardHost,
                                    insert.Item1.ToString(),
                                    String.Empty,
                                    true);

                                insert.Item2
                                    .ObserveOn(SynchronizationContext.Current)
                                    .Subscribe(_ => _loginService.Disconnect());
                            },
                        error => StatusMessage = error.Message);
            Application.Current.Exit  += (s, e) => subscription.Dispose();
        }

        private void UpdateCans(IEnumerable<Can> cans)
        {
            var last = Settings.Default.LastCan;
            Cans.Clear();
            foreach (var can in cans)
            {
                Cans.Add(can);
                if (can == last)
                    SelectedCan = can;
            }

            var selected = Cans.FirstOrDefault(can => can == last);
            if (selected == null && Cans.Count > 0)
                selected = Cans.First();
            SelectedCan = selected;
        }

        public ObservableCollection<Can> Cans { get; private set; }

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


        private Can _selectedCan;

        public Can SelectedCan
        {
            get
            {
                return _selectedCan;
            }
            set
            {
               _selectedCan = value;
              Settings.Default.LastCan = value;

               _loginCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedCan");
                RaisePropertyChanged("CanLogin");
             }
        }
        

        public ICommand LoginCommand 
        {
            get { return _loginCommand; }
        }

        public ICommand ShowSettingsCommand 
        {
            get { return _showSettingsCommand; }
        }

     
        public Settings Settings { get { return Settings.Default; } }

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    RaisePropertyChanged("StatusMessage");
                }
            }
        }

        public bool CanLogin
        {
            get { return _canLogin && SelectedCan != null; }
        }
       
    }
}