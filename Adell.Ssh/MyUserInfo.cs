using System;
using System.Collections.Generic;
using System.Text;
using Tamir.SharpSsh.jsch;

namespace Adell.Ssh
{
    public class MyUserInfo : UserInfo
    {
        /// &lt;summary&gt;
        /// Holds the user password
        /// &lt;/summary&gt;
        private String passwd { get; set; }

        /// &lt;summary&gt;
        /// Returns the user password
        /// &lt;/summary&gt;
        public String getPassword() { return passwd; }

        /// &lt;summary&gt;
        /// Prompt the user for a Yes/No input
        /// &lt;/summary&gt;
        public bool promptYesNo(String str)
        {
            return true;
        }

        /// &lt;summary&gt;
        /// Returns the user passphrase (passwd for the private key file)
        /// &lt;/summary&gt;
        public String getPassphrase() { return null; }

        /// &lt;summary&gt;
        /// Prompt the user for a passphrase (passwd for the private key file)
        /// &lt;/summary&gt;
        public bool promptPassphrase(String message) { return true; }

        /// &lt;summary&gt;
        /// Prompt the user for a password
        /// &lt;/summary&gt;
        public bool promptPassword(String message) { return true; }

        /// &lt;summary&gt;
        /// Shows a message to the user
        /// &lt;/summary&gt;
        public void showMessage(String message) { }

    }
}
