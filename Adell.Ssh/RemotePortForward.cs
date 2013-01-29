using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Adell.Ssh
{
    public class RemotePortForwarding
    {
        public RemotePortForwarding(int remotePort, IPEndPoint destination)
        {
            Destination = destination;
            RemotePort = remotePort;
        }

        public RemotePortForwarding
            (int localPort,
            IPAddress viaLocalToAddress,
            int viaLocalToPort) :
            this(localPort, new IPEndPoint(viaLocalToAddress, viaLocalToPort))
        {
        }

        public RemotePortForwarding(
            int localPort,
            string viaRemoteToAddress,
            int viaRemoteToPort) :
            this(localPort, IPAddress.Parse(viaRemoteToAddress), viaRemoteToPort)
        {
        }

        public IPEndPoint Destination { get; private set; }
        public int RemotePort { get; private set; }
    }

}
