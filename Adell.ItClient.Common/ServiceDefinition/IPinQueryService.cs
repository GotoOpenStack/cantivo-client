using System;

namespace Adell.ItClient.Common.ServiceDefinition
{
    public interface IPinQueryService
    {
        IObservable<string> QueryPin(string reason);
    }
}
