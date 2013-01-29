using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Adell.ItClient.Linux.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /*
        public bool IsInDesigner
        {
            get
            {
                //#warning DesignerProperties depends on WindowsBase... should DIptel.Convenicne link to this?
                //return false;
                return DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }
         */
    }

}
