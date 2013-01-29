using System;
using System.Diagnostics;
using System.Net;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Common.Interfaces;

namespace Adell.ItClient.Common.BaseClasses
{
    //TODO: This needs refactoring, remove old event-system
    public abstract class RemoteDesktopBase : IRemoteDesktop
    {
        public IObservable<Unit> Connect(IPEndPoint endpoint, NetworkCredential credentials)
        {
            return Observable.Create<Unit>(
                obs =>
                {
                    var disp = new CompositeDisposable();
                    var disconnected = Observable.FromEventPattern<EventArgs>(this, "OnDisconnected");
                    try
                    {
                        InternalConnect(endpoint, credentials);
                        var subs = disconnected.Take(1).Subscribe(_ => { }, obs.OnCompleted);
                        disp.Add(subs);
                        disp.Add(Disposable.Create(Disconnect));
                    }
                    catch (Exception ex)
                    {
                        obs.OnError(ex);
                    }
                    return disp;
                });
        }
        private readonly Process _process;
        public event EventHandler<EventArgs> OnDisconnected;

        protected RemoteDesktopBase()
        {
            _process = new Process {EnableRaisingEvents = true};
        }

        protected ProcessStartInfo StartInfo
        {
            get { return _process.StartInfo; }
        }


        public abstract protocol ProtocolType { get; }
        
        public abstract void InternalConnect(IPEndPoint endpoint, NetworkCredential credentials);
        
        public virtual void Disconnect()
        {
            if(!_process.HasExited)
                _process.Kill();
        }

        protected void Start()
        {
            _process.Start();
            _process.WaitForInputIdle();
            _process.Exited += Exited;
            Started(_process);
        }

        protected virtual void Started(Process p){}

        private void Exited(object sender, EventArgs e)
        {
            if (OnDisconnected != null)
                OnDisconnected(this, e);
        }
    }
}
