using System;
using System.Collections.Generic;
using Adell.ItClient.Common.Models;

namespace Adell.ItClient.Common.ServiceDefinition
{
    public interface ICanDiscoveryService
    {
         IObservable<IEnumerable<Can>> Cans { get; }
    }
}
