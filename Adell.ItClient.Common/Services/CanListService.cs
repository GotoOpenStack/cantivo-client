using System;
using System.Collections.Generic;
using System.Net;
using Adell.ItCan.Interop.Entities;
using Adell.ItCan.Interop.Services;
using Adell.ItClient.Common.ServiceDefinition;

namespace Adell.ItClient.Common.Services
{
    public class CanListService : ICanListService
    {
        private static readonly IPEndPoint HttpLocalhost = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);
        //TODO: _isSsh is a HACK!
        private readonly Func<bool> _isSsh;


        public CanListService(Func<bool> isSsh)
        {
            _isSsh = isSsh;
        }

        public IObservable<IEnumerable<desktop>> GetHostListAsync(IPAddress address, NetworkCredential creds)
        {
            IHostListService hostListService;
            if (_isSsh())
                hostListService = PrepareSsh(address, creds);
            else
                hostListService =
                    new HttpHostListService(creds, new IPEndPoint(address, 80));
            return hostListService.GetHostListAsync();
        }

        private static SshTunnelingHostListService PrepareSsh(
            IPAddress host,
            NetworkCredential creds,
            int localPort = 8080)
        {
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

