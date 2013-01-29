using System.Windows.Input;
using Adell.ItClient.Common.Models;
using Adell.ItClient.Windows.Properties;
using System.Collections.ObjectModel;

namespace Adell.ItClient.Windows.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly DelegateCommand<Can> _addCanCommand;
        private readonly DelegateCommand<Can> _removeCanCommand;
       
        public SettingsViewModel()
        {
            Cans = new ObservableCollection<Can>();

            if (Settings.Default.CustomHosts == null)
                Settings.Default.CustomHosts = new CanCollection();
            _addCanCommand = new DelegateCommand<Can>(
                _ =>
                    {
                        var can = new Can("Address", "Name"); 
                       Config.CustomHosts.Add(can);
                       Cans.Add(can);
                    });

            _removeCanCommand = new DelegateCommand<Can>(
                can =>
                    {
                        var selected = can ?? SelectedCan;
                        if (selected != null && Config.CustomHosts.Contains(selected))
                            Config.CustomHosts.Remove(selected);
                        if (selected != null && Cans.Contains(selected))
                            Cans.Remove(selected);
                        RaisePropertyChanged("Config");
                    });

            foreach (var can in Settings.Default.CustomHosts)
            {
                Cans.Add(can);
            }

        }

        public ObservableCollection<Can> Cans { get; set; }

        public Settings Config
        {
            get { return Settings.Default; }
        }

        public Can SelectedCan { get; set; }

        public ICommand AddCanCommand { get { return _addCanCommand; } }
        public ICommand RemoveCanCommand { get { return _removeCanCommand; } }
        
    }
}
