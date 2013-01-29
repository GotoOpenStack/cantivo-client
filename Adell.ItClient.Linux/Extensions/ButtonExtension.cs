using System;
using System.ComponentModel;
using System.Windows.Forms;
using Adell.ItClient.Common.Interfaces;
using Adell.ItClient.Linux.Interfaces;

namespace Adell.ItClient.Linux.Extensions
{
    public static class ButtonExtension
    {
        private class CommandWrapper : INotifyPropertyChanged
        {
            private readonly IFormsCommand _command;
            public CommandWrapper(IFormsCommand command)
            {
                //Allow binding to nullcommands. (no exception)
                if (command != null)
                {
                    _command = command;
                    _command.CanExecuteChanged += (s, e) =>
                    {
                        if (PropertyChanged != null)
                            PropertyChanged(this,
                                            new PropertyChangedEventArgs("CanExecute"));
                    };
                }
            }

            public bool CanExecute { get { return _command.CanExecute(null); } }
            public void Execute(object obj)
            {
                _command.Execute(obj);
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }


        public static void BindCommand(this Button button, IFormsCommand command)
        {
            var wrapper = new CommandWrapper(command);
            // button.DataBindings.Add("Text", wrapper, "DisplayName");
            button.DataBindings.Add("Enabled", wrapper, "CanExecute");
            button.Click += (s, e) => wrapper.Execute(null);
        }

        public static void BindCommand(this Button button, IFormsCommand command, Func<object, object> param)
        {
            var wrapper = new CommandWrapper(command);
            // button.DataBindings.Add("Text", wrapper, "DisplayName");
            button.DataBindings.Add("Enabled", wrapper, "CanExecute");
            button.Click += (s, e) => wrapper.Execute(param);
        }
    }
}
