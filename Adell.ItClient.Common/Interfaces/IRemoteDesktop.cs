using System;
using System.Net;
using System.Reactive;
using Adell.ItCan.Interop.Entities;

namespace Adell.ItClient.Common.Interfaces
{
    public interface IRemoteDesktop
    {
        protocol ProtocolType { get; }
        IObservable<Unit> Connect(IPEndPoint endpoint, NetworkCredential credentials);
    }
}
