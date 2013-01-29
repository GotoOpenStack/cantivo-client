using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Adell.ItClient.Common.Models;
using Adell.ItClient.Common.ServiceDefinition;
using Mono.Zeroconf;

namespace Adell.ItClient.Common.Services
{
    public class CanDiscoveryService : ICanDiscoveryService
    {
        private readonly ServiceBrowser _browser;
        private readonly object _lock = new object();
        private readonly List<Can> _cans;
        private const string DefaultService = "_libvirt._tcp";
        private const string DefaultDomain = "local";
        private Subject<IEnumerable<Can>> _subject;

        public CanDiscoveryService() : this(DefaultService, DefaultDomain)
        {
        }

        public CanDiscoveryService(string mDnsService, string mDnsDomain)
        {
            _cans = new List<Can>();
           // _cans.Add(new Can("skynet.io", "skynet.io"));

            try
            {
                _browser = new ServiceBrowser();
                _browser.ServiceAdded += ServiceAdded;
            
                _subject = new Subject<IEnumerable<Can>>();
                var conn = _subject.Replay(1);
                conn.Connect();
                Cans = conn;
            
                _browser.Browse(0,
                        AddressProtocol.Any,
                        mDnsService,
                        mDnsDomain);


            }
            catch (Exception ex)
            {
                Cans = Observable.Throw<IEnumerable<Can>>(ex);
            }
        }

        public IObservable<IEnumerable<Can>> Cans { get; private set; }


        private void ServiceAdded(object o, ServiceBrowseEventArgs args)
        {
            args.Service.Resolved += ServiceResolved;
            //Service.Resolve() blocks in Windows.Forms on .NET-clr
            Task.Factory.StartNew(() => args.Service.Resolve());
          }

        private void ServiceResolved(object o, ServiceResolvedEventArgs args)
        {
            var ip = args.Service.HostEntry.AddressList[0].ToString();
            lock (_lock)
            {
                //Dont add ITCan if its already present
                if (_cans.FirstOrDefault(d => d.Name == args.Service.Name) != null)
                    return;

                _cans.Add(new Can(ip,args.Service.Name));
                _subject.OnNext(_cans.ToArray());
            }
           
        }
    }
}
