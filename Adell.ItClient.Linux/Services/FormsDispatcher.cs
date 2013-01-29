using System;
using System.Windows.Forms;
using Adell.ItClient.Common.ServiceDefinition;

namespace Adell.ItClient.Linux.Services
{
    public class FormsDispatcher : IDispatcher
    {
        private readonly Control _control;
        public FormsDispatcher(Control control)
        {
            _control = control;
        }

        public void Dispatch(Action action)
        {
            if (_control.InvokeRequired)
                _control.Invoke(action);
            else
                action();
        }
    }
}