//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Windows.Forms;
//using MSTSCLib;

//namespace Adell.ItClient.Linux.Win32.RemoteDesktop.Win32
//{
//    //public class RdpHandled : IRemoteDesktop
//    //{
//    //    private IMsRdpClient _client;
//    //    public void Disconnect()
//    //    {
//    //        if (_client != null && _client.Connected != 0)
//    //            _client.Disconnect();
//    //    }

//    //    public event EventHandler<DesktopDisconnectedEventArgs> OnDisconnected;

//    //    public void Connect(IPEndPoint endpoint, NetworkCredential credential)
//    //    {
//    //       RdpForm r = new RdpForm();
//    //       r.Show();
//    //        r.WindowState = FormWindowState.Maximized;
//    //        r.Size = r.MaximumSize;
//    //        r.Client.UserName = credential.UserName;
//    //        r.Client.Server = endpoint.Address.ToString();
//    //        IMsTscNonScriptable secured = (IMsTscNonScriptable)r.Client.GetOcx();
//    //        secured.ClearTextPassword = credential.Password;
//    //        r.Client.Connect();
//    //      //  r.Client.OnDisconnected += HandleDisconnected;
//    //        //r.Client.FullScreen = true;
//    //    }

//    //    //void HandleDisconnected(object sender, IMsTscAxEvents_OnDisconnectedEvent e)
//    //    //{
//    //    //    if (OnDisconnected  != null)
//    //    //        OnDisconnected(this, new DesktopDisconnectedEventArgs());
//    //    //}

//    //}
//}
