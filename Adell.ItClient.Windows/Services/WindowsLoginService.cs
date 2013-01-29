using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Adell.Convenience.Extensions;
using Adell.ItCan.Interop.Entities;
using Adell.ItCan.Interop.Services;
using Adell.ItClient.Windows.Properties;
using Adell.ItClient.Common.ServiceDefinition;
using Adell.ItClient.Common.Services;
using Adell.ItClient.Windows.ViewModels;
using Adell.ItClient.Windows.Windows;

namespace Adell.ItClient.Windows.Services
{
    public class WindowsLoginService : ILoginService
    {
        private CompositeDisposable _resources;
        private Subject<LoginState> _loginState;
        private IDesktopFactory _desktopFactory;

        public WindowsLoginService()
        {
            _loginState = new Subject<LoginState>();
            _desktopFactory = new DesktopFactory();
        }

        public IObservable<LoginState> StateChanged
        {
            get { return _loginState; }
        }

        public void Connect(string hostName, string username, string password, bool cardLogin)
        {
            if(!cardLogin)
                Settings.Default.Save();
            _loginState.OnNext(LoginState.Connecting);
            _resources = new CompositeDisposable();

            var serverListModel = new CanListService(() => Settings.Default.UseSsh);
        
           var pipeline = serverListModel.GetHostListAsync(hostName, username, password);
           // var pipeline = new MockHostListService(10).GetHostListAsync();
           //var pipeline = new MockHostListService(1, protocol.spice).GetHostListAsync();


           _resources.Add(pipeline.Subscribe(
               GotCanDesktops, 
               ex => GetCanDesktopsFailed(hostName, username, password, cardLogin, ex)));
        }

        public void Disconnect()
        {
            if (_resources != null)
                _resources.Dispose();
            _loginState.OnNext(LoginState.Disconnected);
        }

        private void GetCanDesktopsFailed(
            string hostName,
            string username,
            string password,
            bool cardLogin,
            Exception ex)
        {
            var wx = ex as WebException;
            if (wx != null && wx.Status == WebExceptionStatus.ProtocolError && cardLogin)
            {
                var ask = new AskPinDialog();
                _resources.Add(Disposable.Create(ask.Close));
                var result = ask.ShowDialog();
                var pin = ask.Pin.SecurePassword.String();
                if (result.HasValue && result.Value)
                    Connect(hostName, username, pin, true);
            }
            else
            {
                _loginState.OnNext(new LoginState(LoginStatus.Disconnected, ex));
            }
        }

        private void GotCanDesktops(IEnumerable<desktop> desktops)
        {
            if(desktops.Count() == 1)
            {
                var desktop = desktops.First();
                DoConnect(desktop);
            }
            else if(desktops.Count() > 1)
            {
                var viewModel = new RemoteDesktopSelectViewModel(desktops);
                var window = new RemoteDesktopSelectWindow(viewModel);
                //_resources.Add(Disposable.Create(() => _dispatcher.Dispatch(window.Close)));
                window.ShowDialog();
                if (window.DialogResult.HasValue && 
                    window.DialogResult.Value &&
                    viewModel.SelectedDesktop != null)
                    DoConnect(viewModel.SelectedDesktop);
            }
        }

        private void DoConnect(desktop desktop)
        {
            var desktopConnection = _desktopFactory.Factory(desktop.type);
            var subs = desktopConnection
                .Connect(desktop.GetEndpoint(), new NetworkCredential())
                .Subscribe(_ => { },
                           ex => _loginState.OnNext(new LoginState(LoginStatus.Disconnected, ex)),
                           () => _loginState.OnNext(LoginState.Disconnected));
            _resources.Add(subs);
        }
    }
}
