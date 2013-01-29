using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Adell.Ssh
{
    public class LocalPortForwarding
    {
        public LocalPortForwarding(int localPort, IPEndPoint destination)
        {
            Destination = destination;
            LocalPort = localPort;
        }

        public LocalPortForwarding
            (int localPort,
            IPAddress viaRemoteToAddress,
            int viaRemoteToPort) :
            this(localPort, new IPEndPoint(viaRemoteToAddress, viaRemoteToPort))
        {
        }

        public LocalPortForwarding(
            int localPort,
            string viaRemoteToAddress,
            int viaRemoteToPort) :
            this(localPort, IPAddress.Parse(viaRemoteToAddress), viaRemoteToPort)
        {
        }

        public IPEndPoint Destination { get; private set; }
        public int LocalPort { get; private set; }
    }
}
