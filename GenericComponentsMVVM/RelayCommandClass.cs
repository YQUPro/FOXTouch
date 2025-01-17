using System;
using System.Windows.Input;

namespace GenericComponentsMVVM
{
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class RelayCommand<T1, T2> : ICommand
    {
        private readonly Action<T1, T2> _execute;
        private readonly Func<T1, T2, bool> _canExecute;

        public RelayCommand(Action<T1, T2> execute, Func<T1, T2, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            if (parameter is Tuple<T1, T2> tuple)
                return _canExecute(tuple.Item1, tuple.Item2);

            return false;
        }

        public void Execute(object parameter)
        {
            if (parameter is Tuple<T1, T2> tuple)
                _execute(tuple.Item1, tuple.Item2);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class RelayCommandWithResult<T, TResult> : ICommand
    {
        private readonly Func<T, TResult> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommandWithResult(Func<T, TResult> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute((T)parameter);

        public void Execute(object parameter) => _execute((T)parameter);

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class RelayCommandWithResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> : ICommand
    {
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> _execute;
        private readonly Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, bool> _canExecute;

        public RelayCommandWithResult(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> execute, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            if (parameter is Tuple<Tuple<T1, T2, T3, T4, T5, T6 >, Tuple<T7, T8,T9, T10, T11, T12>, Tuple< T13, T14, T15, T16>> tuple)
                return _canExecute(tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item1.Item3, tuple.Item1.Item4, tuple.Item1.Item5, tuple.Item1.Item6,
                                   tuple.Item2.Item1, tuple.Item2.Item2, tuple.Item2.Item3, tuple.Item2.Item4, tuple.Item2.Item5, tuple.Item2.Item6,
                                   tuple.Item3.Item1, tuple.Item3.Item2, tuple.Item3.Item3, tuple.Item3.Item4
                                   );

            return false;
        }

        public void Execute(object parameter)
        {
            if (parameter is Tuple<Tuple<T1, T2, T3, T4, T5, T6>, Tuple<T7, T8, T9, T10, T11, T12>, Tuple<T13, T14, T15, T16>> tuple)
                _execute(tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item1.Item3, tuple.Item1.Item4, tuple.Item1.Item5, tuple.Item1.Item6,
                                   tuple.Item2.Item1, tuple.Item2.Item2, tuple.Item2.Item3, tuple.Item2.Item4, tuple.Item2.Item5, tuple.Item2.Item6,
                                   tuple.Item3.Item1, tuple.Item3.Item2, tuple.Item3.Item3, tuple.Item3.Item4);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
