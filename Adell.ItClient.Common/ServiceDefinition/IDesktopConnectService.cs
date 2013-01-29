using System;
using System.Reactive;
using Adell.ItCan.Interop.Entities;

namespace Adell.ItClient.Common.ServiceDefinition
{
    public interface IDesktopConnectService
    {
        
        //TODO: Move to async-keyword in C# 5
        //OnError if errors, OnComplete when disconnected, Nothing when running
        IObservable<Unit> Connect(desktop desktop);
    }
}