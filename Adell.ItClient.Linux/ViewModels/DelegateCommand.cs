using System;
using Adell.ItClient.Common.Interfaces;
using Adell.ItClient.Linux.Interfaces;

namespace Adell.ItClient.Linux.ViewModels
{
    public class DelegateCommand<T> : IFormsCommand
    {
        private readonly Func<T, bool> _canExecute;
        private readonly Action<T> _execute;

        public DelegateCommand(Action<T> execute) 
            : this(execute, t => true)
        {
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _execute.Invoke((T)parameter);

        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}