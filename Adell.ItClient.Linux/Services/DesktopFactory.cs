using System;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Common.Interfaces;
using Adell.ItClient.Common.ServiceDefinition;

namespace Adell.ItClient.Linux.Services
{
    internal class DesktopFactory : IDesktopFactory
    {
        public IRemoteDesktop Factory(protocol protocol)
        {
            switch (protocol)
            {
                case protocol.rdp:
                    throw new NotImplementedException();
                case protocol.spice:
                    return new SpiceLinux();
                case protocol.vnc:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
