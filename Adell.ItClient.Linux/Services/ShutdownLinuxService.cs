using System;
using System.IO;
using Adell.ItClient.Common.ServiceDefinition;

namespace Adell.ItClient.Linux.Services
{
    class ShutdownLinuxService : IShutdownService
    {
        private const byte ShutdownCmd = 0x6f;
        private const byte RebootCmd = 0x62;

        private static void Sysreq(byte command)
        {
            try
            {
                var f = File.OpenWrite("/proc/sysrq-trigger");
                f.WriteByte(command);
                f.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Shutdown() { Sysreq(ShutdownCmd); }
        public void Reboot() { Sysreq(RebootCmd); }

    }


}
