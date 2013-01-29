using Adell.ItClient.Common.ServiceDefinition;
using Adell.ItClient.Common.Services;
using Adell.ItClient.Linux.Interfaces;

namespace Adell.ItClient.Linux.ViewModels
{
    public class SystemViewModel : BaseViewModel
    {
        public SystemViewModel()
        {
            CanShutdown = ServiceLocator.IsRegistered<IShutdownService>();
            if (CanShutdown)
            {
                var shutdownService = ServiceLocator.GetService<IShutdownService>();
                ShutdownCommand = new DelegateCommand<object>(
                    ignore => shutdownService.Shutdown());
                RebootCommand = new DelegateCommand<object>(
                    ignore => shutdownService.Reboot());
            }
        }

        public bool CanShutdown { get; private set; }

        public IFormsCommand RebootCommand { get; private set; }
        public IFormsCommand ShutdownCommand { get; private set; }
    }
}
