using System;
using System.Net;
using Adell.ItCan.Interop.Entities;
using Adell.ItClient.Common.BaseClasses;

namespace Adell.ItClient.Windows.Services
{
    public class TightVnc : RemoteDesktopBase
    {
        const string Path = "ExternalBinaries\\TightVnc\\vncviewer.exe";

        public override protocol ProtocolType
        {
            get { return protocol.vnc; }
        }

        public override void InternalConnect(IPEndPoint endpoint, NetworkCredential credentials)
        {
            var basepath = System.IO.Path.GetDirectoryName(App.ResourceAssembly.Location);
            var path = basepath + "/" + Path;
            StartInfo.FileName = path;
            StartInfo.Arguments = String.Format("{0}:{1}", endpoint.Address, endpoint.Port);
            Start();
        }
    }
}
