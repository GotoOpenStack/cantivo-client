using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Windows.Forms;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Linux.ViewModels;
using Adell.ItClient.Linux.Windows;
using Adell.ItClient.Common.ServiceDefinition;
using Adell.ItClient.Common.Services;

namespace Adell.ItClient.Linux.Services
{
    public class LinuxLoginService : ILoginService
    {
        private readonly IDispatcher _dispatcher;
        private CompositeDisposable _resources;
        private Subject<LoginState> _loginState;

        public LinuxLoginService(IDispatcher dispatcher)
        {
            _loginState = new Subject<LoginState>();
            _dispatcher = dispatcher;
        }
        public IObservable<LoginState> StateChanged
        {
            get { return _loginState; }
        }

        public void Connect(string hostName, string username, string password, bool cardLogin)
        {
            _loginState.OnNext(LoginState.Connecting);
            _resources = new CompositeDisposable();

            var pipeline =
                ServiceLocator
                .GetService<ICanListService>()
                .GetHostListAsync(hostName, username, password);

            //var pipeline = new MockHostListService(10).GetHostListAsync();
      

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
                _dispatcher.Dispatch(
                    () =>
                        {
                            var ask = new AskPinDialog();
                            _resources.Add(Disposable.Create(
                                () => _dispatcher.Dispatch(ask.Close)));
                            if (!ask.IsDisposed)
                            {
                                var result = ask.ShowDialog();
                                if (result == DialogResult.Retry)
                                {
                                    Connect(hostName, username, ask.Pin, true);
                                }
                            }
                        });
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
                try
                {
                    var desktop = desktops.First();
                    ConnectDesktop(desktop);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if(desktops.Count() > 1)
            {
                var viewModel = new RemoteDesktopSelectViewModel(desktops);
                var window = new RemoteDesktopSelectWindow(viewModel);
                _resources.Add(Disposable.Create(
                    () => _dispatcher.Dispatch(window.Close)));

                window.ShowDialog(); //TODO: implement dialogresult
                if(viewModel.SelectedDesktop != null)
                    ConnectDesktop(viewModel.SelectedDesktop);
            }
        }

        private void ConnectDesktop(desktop desktop)
        {
            var desktopConnection =
                ServiceLocator.GetService<IDesktopFactory>().Factory(desktop.type);

            var subs =
                desktopConnection.Connect(desktop.GetEndpoint(), new NetworkCredential())
                    .Subscribe(_ => { },
                               ex => _loginState.OnNext(new LoginState(LoginStatus.Disconnected, ex)),
                               () => _loginState.OnNext(LoginState.Disconnected));
            _resources.Add(subs);
        }
    }
}
