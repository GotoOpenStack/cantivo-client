using System;
using System.Windows.Forms;
using Adell.ItClient.Common.ServiceDefinition;
using Adell.ItClient.Common.Services;
using Adell.ItClient.Linux.Properties;
using Adell.ItClient.Linux.Services;

namespace Adell.ItClient.Linux
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            boot();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        static void boot()
        {
            ServiceLocator.RegisterService<IShutdownService>(new ShutdownLinuxService());
            ServiceLocator.RegisterService<IDesktopFactory>(new DesktopFactory());
            ServiceLocator.RegisterService<ICanListService>(new CanListService(() => Settings.Default.UseSsh));
        }
    }
}
