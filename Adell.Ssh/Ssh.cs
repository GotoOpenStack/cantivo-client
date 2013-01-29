using System;
using System.Collections.Generic;
using System.Text;
using Tamir.SharpSsh.jsch;
using System.Net;

namespace Adell.Ssh
{
    public static class Ssh
    {
        private static JSch _jsch;

        static Ssh()
        {
           _jsch = new JSch();
        }

        public static SshSession GetSession(IPEndPoint endpoint, NetworkCredential credentials)
        {
            var sshsession = _jsch.getSession(credentials.UserName, endpoint.Address.ToString(), endpoint.Port);
            var session = new SshSession(sshsession);
            sshsession.setHost(endpoint.Address.ToString());
            sshsession.setPassword(credentials.Password);
            session.Userinfo = new MyUserInfo();
            return session;
        }
    }
}
