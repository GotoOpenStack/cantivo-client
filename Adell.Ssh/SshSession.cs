using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Adell.Ssh
{
    public class SshSession
    {
        private readonly Tamir.SharpSsh.jsch.Session _session;
        private bool _userInfoSet;
        public SshSession(Tamir.SharpSsh.jsch.Session session)
        {
            _session = session;
            _userInfoSet = false;
        }

        public Tamir.SharpSsh.jsch.UserInfo Userinfo
        {
            set
            {
                _userInfoSet = true;
                _session.setUserInfo(value);
            }
        }

        public void Disconnect()
        {
            _session.disconnect();
        }

        public void AddPortForwarding(LocalPortForwarding remote)
        {
            _session.setPortForwardingL(remote.LocalPort, remote.Destination.Address.ToString(), remote.Destination.Port);
        }

        public void RemovePortForwarding(LocalPortForwarding remote)
        {
            RemovePortForwarding(remote.LocalPort);
        }

        public void RemovePortForwarding(int remotePort)
        {
            _session.delPortForwardingL(remotePort);
        }

        public bool IsConnected
        {
            get { return _session.isConnected(); }
        }

        public int Timeout
        {
            get
            {
                return _session.getTimeout();
            }
            set
            {
                _session.setTimeout(value);
            }
        }

        public void Connect()
        {
            if (IsConnected)
                throw new InvalidOperationException("Already connected");
            if (!_userInfoSet)
                throw new InvalidOperationException("UserInfo must be set before Connecting");
            _session.connect();
        }

        public void Connect(int timeout)
        {
            if (IsConnected)
                throw new InvalidOperationException("Already connected");
            if (!_userInfoSet)
                throw new InvalidOperationException("UserInfo must be set before Connecting");
            _session.connect(timeout);
        }
    }
}
