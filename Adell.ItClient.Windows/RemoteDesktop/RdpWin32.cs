using System;
using Adell.Convenience;
using Adell.Convenience.Extensions;
using System.Net;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Common.BaseClasses;

namespace Adell.ItClient.Windows.RemoteDesktop
{
    public class RdpWin32 : RemoteDesktopBase
    {
        private class Args
        {
            /// <summary>
            /// Specifies the remote computer to which you want to connect
            /// </summary>
            public const string Server = "/v:";

            /// <summary>
            /// Connects you to the session for administering a server
            /// </summary>
            public const string Admin = "/v:";

            /// <summary>
            /// Starts Remote Desktop in full-screen mode
            /// </summary>
            public const string Fullscreen = "/f";

            /// <summary>
            /// Specifies the Height of the Remote Desktop window
            /// </summary>
            public const string Height = "/h:";

            /// <summary>
            /// Specifies the Width of the Remote Desktop window
            /// </summary>
            public const string Width = "/w:";

            /// <summary>
            /// Matches the remote width and height with the local
            /// virtual desktop, spanning across multiple monitors,
            /// if necessary. To span across monitors, the monitors
            /// must be arranged to form a rectangle.
            /// </summary>
            public const string Span = "/span";

            /// <summary>
            /// Configures the remote desktop session monitor layout
            /// to be identical to the current client-side configuration.
            /// </summary>
            public const string MultiMon = "/multimon";

            //Todo: /edit /migrate
        }

        public override protocol ProtocolType
        {
            get { return protocol.rdp; }
        }

        public override void InternalConnect(IPEndPoint endpoint, NetworkCredential credential)
        {
            var path = Environment.GetEnvironmentVariable("windir");
            StartInfo.FileName = path + "\\system32\\mstsc.exe";

            var serverArg = String.Format("{0}:{1}", endpoint.Address, endpoint.Port );

            StartInfo.Arguments +=
                Arguments.AddArgument(Args.Server, serverArg);
            Start();
        }
    }
}
