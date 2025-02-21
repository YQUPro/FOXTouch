using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GenericComponentsMVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Dictionary<string, RelayCommand> _commands;
        public Dictionary<string, RelayCommand> Commands
        {
            get => _commands;
            set
            {
                if (_commands != value)
                {
                    _commands = value;
                    OnPropertyChanged(nameof(Commands));
                }
            }
        }
    }
}