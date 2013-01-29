using System.Net;

namespace Adell.ItCan.Interop.Entities
{
    public static class DesktopExtensions
    {
        public static IPEndPoint GetEndpoint(this desktop desktop)
        {
            return new IPEndPoint(IPAddress.Parse(desktop.ip), desktop.port);
        }
    }
}
