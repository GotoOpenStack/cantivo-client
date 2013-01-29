using System;
using System.Net;
using System.Collections.Generic;
using Adell.ItCan.Interop.Entities;
using Adell.Ssh;
namespace Adell.ItCan.Interop.Services
{
    public class SshTunnelingHostListService : IHostListService
    {
        private LocalPortForwarding _localPortForwarding;
        private IPEndPoint _host;
        private NetworkCredential _networkCredential;
        private HttpHostListService _http;
        private SshSession _session;

        public SshTunnelingHostListService(
            int localPort,
            IPEndPoint destinationEndpoint,
            IPAddress sshHost,
            NetworkCredential sshCredential,
            HttpHostListService http)
            : this(new LocalPortForwarding(localPort, destinationEndpoint),
            new IPEndPoint(sshHost, 22), sshCredential, http)
        {
        }


        private SshTunnelingHostListService(
            LocalPortForwarding localPortForwarding,
            IPAddress host,
            NetworkCredential networkCredential,
            HttpHostListService http)
            : this(localPortForwarding, new IPEndPoint(host, 22), networkCredential, http)
        {
        }

        private SshTunnelingHostListService(
            LocalPortForwarding localPortForwarding,
            IPEndPoint host,
            NetworkCredential networkCredential,
            HttpHostListService http)
            : this(
            localPortForwarding,
            host,
            networkCredential,
            http,
            Ssh.Ssh.GetSession(host, networkCredential))
        {
        }

        private SshTunnelingHostListService(
            LocalPortForwarding localPortForwarding,
            IPEndPoint host,
            NetworkCredential networkCredential,
            HttpHostListService http,
            SshSession session)
        {
            _localPortForwarding = localPortForwarding;
            _host = host;
            _networkCredential = networkCredential;
            _http = http;
            _session = session;
        }

        public IObservable<IEnumerable<desktop>> GetHostListAsync()
        {
            bool doConnect = !_session.IsConnected;
            if (doConnect)
                _session.Connect();
            try
            {
                _session.AddPortForwarding(_localPortForwarding);
                return _http.GetHostListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                _session.RemovePortForwarding(_localPortForwarding);
                if (doConnect)
                    _session.Disconnect();
            }
        }
    }
}