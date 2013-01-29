using System;

namespace Adell.ItClient.Common.ServiceDefinition
{
    public enum LoginStatus
    {
        Conneting,
        Connected,
        Disconnected
    }

    public class LoginState
    {
        public static LoginState Connecting { get { return new LoginState(LoginStatus.Conneting); } }
        public static LoginState Disconnected { get { return new LoginState(LoginStatus.Disconnected); } }

        public LoginState(LoginStatus loginStatus) : this(loginStatus, null) { }
        public LoginState(LoginStatus loginStatus, Exception error)
        {
            Status = loginStatus;
            Error = error;
        }

        public LoginStatus Status { get; private set; }
        public Exception Error { get; private set; }
    }

    public interface ILoginService
    {
        IObservable<LoginState> StateChanged { get; }
        void Connect(string hostName, string user, string password, bool cardLogin = false);
        void Disconnect();
    }
}
