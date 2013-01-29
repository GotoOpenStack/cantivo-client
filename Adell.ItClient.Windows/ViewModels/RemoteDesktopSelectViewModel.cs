using System;
using System.Collections.Generic;
using Adell.ItCan.Interop.Entities;

namespace Adell.ItClient.Windows.ViewModels
{
    public class RemoteDesktopSelectViewModel : BaseViewModel
    {
        public RemoteDesktopSelectViewModel(IEnumerable<desktop> desktops)
        {
            Desktops = desktops;
        }
        private desktop _selectedDesktop;

        public desktop SelectedDesktop
        {
            get { return _selectedDesktop; }
            set
            {
                _selectedDesktop = value;
                RaisePropertyChanged("SelectedDesktop");
            }
        }

        public IEnumerable<desktop> Desktops { get; set; }
    }

}
