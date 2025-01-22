using GenericComponentsMVVM;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace FOXTouch_WPF.ViewModels
{
    public class SimplifiedLightsViewModel : ViewModelBase
    {
        private Dictionary<string, RelayCommand> _commands;
        public Dictionary<string, RelayCommand> Commands
        {
            get => _commands;
            set
            {
                if (_commands != value)
                {
                    _commands = value;
                    OnPropertyChanged(nameof(Commands));
                    Console.WriteLine("Commands property changed in SimplifiedLightsViewModel");
                    Console.WriteLine($"SimplifiedLightsWindow used: {this.GetHashCode()}");
                    foreach (var command in _commands)
                    {
                        Console.WriteLine($"Command: {command.Key}");
                    }
                }
            }
        }

        public ICommand ToggleSimplifiedEpiscopicLightViewCommand { get; }

        public SimplifiedLightsViewModel()
        {
            
            ToggleSimplifiedEpiscopicLightViewCommand = new RelayCommand(ToggleSimplifiedEpiscopicLightViewFunction);
            Console.WriteLine($"SimplifiedLightsWindow DataContext assigned: {this.GetHashCode()}");
        }

        private void ToggleSimplifiedEpiscopicLightViewFunction()
        {
            Console.WriteLine($"SimplifiedLightsWindow used: {this.GetHashCode()}");
            Commands["ToggleSimplifiedEpiscopicLightViewCommand"].Execute(null);
        }

        public void NotifyCommandsChanged()
        {
            OnPropertyChanged(nameof(Commands));
        }

        public void ExecuteLightCommand()
        {
            // Action pour la commande des lumières
            Console.WriteLine("Executing Light Command");
        }
    }
}
