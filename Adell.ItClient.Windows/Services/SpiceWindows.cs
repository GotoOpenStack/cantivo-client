using System;
using System.Net;
using System.Diagnostics;
using Adell.Convenience;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Common.BaseClasses;

namespace Adell.ItClient.Windows.Services
{
    public class SpiceWindows : RemoteDesktopBase
    {
        public static class Args
        {
            public const string Host = "-h";
            public const string Port = "-p";
            public const string User = "-u";
            public const string Pass = "-w";
            public const string Fullscreen = "--full-screen=auto-conf";
            public const string SecurePort = "-s";
        }

        const string DefaultPath = "ExternalBinaries/Spice/spicec.exe";



        public SpiceWindows()
        {
            CustomArgs = String.Empty;
            FullScreen = false;
        }

      


        public bool FullScreen { get; set; }
        public string CustomArgs { get; set; }


        public override protocol ProtocolType
        {
            get { return protocol.spice; }
        }

        public override void InternalConnect(IPEndPoint endpoint, NetworkCredential credentials)
        {
            StartInfo.UseShellExecute = true;
            //StartInfo.RedirectStandardOutput = true;
            //StartInfo.CreateNoWindow = true;
            //StartInfo.WindowStyle = ProcessWindowStyle.Hidden;


            //StartInfo.FileName = DefaultPath;
            StartInfo.FileName = "ExternalBinaries\\Spice\\spicec.exe";
            //StartInfo.FileName = @"C:\windows\notepad.exe";
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

        //    
       
    }
}
