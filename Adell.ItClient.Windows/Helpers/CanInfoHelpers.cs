using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Adell.ItCan.Interop.Entities;
using Adell.ItCan.Interop.Services;

namespace Adell.ItClient.Windows.Helpers
{
    public static class CanInfoHelpers
    {
        private static IPEndPoint HttpLocalhost = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);


        public static IObservable<IEnumerable<desktop>> GetInfo(IPAddress address, string username, string password, bool useSsh=false)
        {
            return GetInfo(address, new NetworkCredential(username, password), useSsh);
        }

        public static IObservable<IEnumerable<desktop>> GetInfo(IPAddress address, NetworkCredential creds, bool useSsh=false)
        {
            IHostListService boundHostListService;
            if (useSsh)
                boundHostListService = PrepareSsh(address, creds);
            else
                boundHostListService =
                    new HttpHostListService(creds, new IPEndPoint(address, 80));

            //boundHostListService = new MockHostListService();
            return boundHostListService.GetHostListAsync();
        }

        private static SshTunnelingHostListService PrepareSsh(IPAddress host, NetworkCredential creds)
        {
            var localPort = 8080;
            var http = new HttpHostListService(
                new NetworkCredential("admin", "admin"),
                new IPEndPoint(IPAddress.Loopback, localPort));

            return new SshTunnelingHostListService(
              localPort,
              HttpLocalhost,
              host,
              new NetworkCredential(creds.UserName, creds.Password),
              http);
        }
    }
}
