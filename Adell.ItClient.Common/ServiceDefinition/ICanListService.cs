using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using Adell.Convenience.Extensions;
using Adell.ItCan.Interop.Entities;

namespace Adell.ItClient.Common.ServiceDefinition
{
    public interface ICanListService
    {
        IObservable<IEnumerable<desktop>> GetHostListAsync(IPAddress address, NetworkCredential creds);
    } 

    public static class CanListServiceExtensions
    {
        public static IObservable<IEnumerable<desktop>> GetHostListAsync(
            this ICanListService service, IPAddress address, string username, string password)
        {
            return service.GetHostListAsync(address, new NetworkCredential(username, password));
        }

        public static IObservable<IEnumerable<desktop>> GetHostListAsync(
           this ICanListService service, string hostname, string username, string password)
        {
            IPAddress address;
            if (IPAddress.TryParse(hostname, out address))
                return service.GetHostListAsync(address, new NetworkCredential(username, password));
            else
            {
                var lookup =
                    Observable.FromAsyncPattern<string, IPHostEntry>
                        (Dns.BeginGetHostEntry, Dns.EndGetHostEntry);

                
                return
                    from l in lookup(hostname)
                        .Assert(l => l.AddressList.Count() > 0, l => new Exception("Unknown Host"))
                    let addr = l.AddressList.First()
                    from list in service.GetHostListAsync(addr, username, password)
                    select list;
            }
           
        }
    }

}
