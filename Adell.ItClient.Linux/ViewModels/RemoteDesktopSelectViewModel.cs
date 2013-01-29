using System.Collections.Generic;
using System.ComponentModel;
using Adell.ItCan.Interop.Entities;
using Adell.Convenience.Extensions;

namespace Adell.ItClient.Linux.ViewModels
{
    public class RemoteDesktopSelectViewModel
    {
        public RemoteDesktopSelectViewModel(IEnumerable<desktop> desktops)
        {
            Desktops = new BindingList<desktop>();
            desktops.Run(item => Desktops.Add(item));
        }
        public desktop SelectedDesktop { get; set; }
        public BindingList<desktop> Desktops { get; private set; }
    }
}
