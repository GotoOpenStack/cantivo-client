using System;
using Adell.ItClient.Common.Interfaces;
using Adell.ItClient.Common.ServiceDefinition;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Windows.RemoteDesktop;

namespace Adell.ItClient.Windows.Services
{
    internal class DesktopFactory : IDesktopFactory
    {
        public IRemoteDesktop Factory(protocol protocol)
        {
            switch (protocol)
            {
                case protocol.rdp:
                    return new RdpWin32();
                case protocol.spice:
                    return new SpiceWindows()
                               {
                                   FullScreen = Properties.Settings.Default.FullScreen
                               };
                case protocol.vnc:
                    return new TightVnc();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
