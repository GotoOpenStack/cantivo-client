using System;

namespace Adell.ItClient.Linux.Interfaces
{
   public interface IFormsCommand
    {
        void Execute(object parameter);
        bool CanExecute(object parameter);
        event EventHandler CanExecuteChanged;
    }
}