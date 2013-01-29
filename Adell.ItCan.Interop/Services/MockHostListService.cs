using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Adell.ItCan.Interop.Entities;

namespace Adell.ItCan.Interop.Services
{
    public class MockHostListService : IHostListService
    {
        private readonly IEnumerable<desktop> _desktops;
        public MockHostListService(int count, protocol? proto = null)
        {
            _desktops = from n in Enumerable.Range(0, count)
                        let num = n.ToString()
                        select new desktop
                                   {
                                       name = "Kjell" + num,
                                       ip = "192.168.1." + num,
                                       port = 2000 + n,
                                       type = proto.HasValue ? proto.Value : (protocol) (n%3)
                                   };

        }
        public IObservable<IEnumerable<desktop>> GetHostListAsync()
        {
            return Observable.Return(_desktops);
        }

    }
}
