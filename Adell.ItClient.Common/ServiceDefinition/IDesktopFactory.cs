using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Common.Interfaces;

namespace Adell.ItClient.Common.ServiceDefinition
{
    public interface IDesktopFactory
    {
        IRemoteDesktop Factory(protocol protocol);
    }
}