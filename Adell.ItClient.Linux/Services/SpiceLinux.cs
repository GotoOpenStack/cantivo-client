using System;
using System.Diagnostics;
using System.Net;
using Adell.Convenience;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Common.BaseClasses;
namespace Adell.ItClient.Linux.Services
{
    public class SpiceLinux : RemoteDesktopBase
    {
        public static class Args
        {
            public const string Host = "-h";
            public const string Port = "-p";
            public const string User = "-u";
            public const string Pass = "-w";
            public const string Fullscreen = "-f";
            public const string SecurePort = "-s";
        }

        public SpiceLinux()
        {
            StartInfo.UseShellExecute = false;
            StartInfo.RedirectStandardOutput = true;
            StartInfo.CreateNoWindow = true;
            StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        }

        public override void InternalConnect(IPEndPoint endpoint, NetworkCredential credentials)
        {
            if (String.IsNullOrEmpty(Path))
                throw new InvalidOperationException("FileName must be set");
            StartInfo.FileName = Path;
            StartInfo.Arguments = BuildArgs(endpoint, credentials);
            Start();
        }

        private string BuildArgs(IPEndPoint endpoint, NetworkCredential credentials)
        {
            var args = CustomArgs + " ";
            if (endpoint == null)
                throw new InvalidProgramException("Endpoint must be set before calling Connect()");

            args += Arguments.AddArgument(Args.Host, endpoint.Address);
            args += Arguments.AddArgument(Args.Port, endpoint.Port);

            if (credentials != null)
            {
                if (!String.IsNullOrEmpty(credentials.UserName))
                {
                    args += Arguments.AddArgument(Args.User, credentials.UserName);
                    //No point in checking password if no username specified.
                    if (!String.IsNullOrEmpty(credentials.UserName))
                        args += Arguments.AddArgument(Args.Pass, credentials.Password);
                }
            }

            if (FullScreen)
                args += Arguments.AddSwitch(Args.Fullscreen);
            return args;
        }

        protected string Path
        {
           // get { return "Spice\\spicec.exe"; }
            get { return Properties.Settings.Default.WindowsPath; }
        }

        protected string CustomArgs
        {
            get { return Properties.Settings.Default.WindowsArgs; }
        }

        protected bool FullScreen
        {
            get { return Properties.Settings.Default.FullScreen; }
        }

        public bool Available
        {
            get { return Misc.IsWindowsOS; }
        }

        public override protocol ProtocolType
        {
            get { return protocol.spice; }
        }
    }
}