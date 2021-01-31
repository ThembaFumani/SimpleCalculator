using System;
using System.Windows.Input;

namespace SimpleCalculator
{
    public class RelayCommand<T> : ICommand
    {
        private Action<T> _execute;
        private Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter = null)
        {
            _execute((T)parameter);
        }
    }
}
