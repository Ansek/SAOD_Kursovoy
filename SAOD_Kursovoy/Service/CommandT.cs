using System;
using System.Windows.Input;

namespace SAOD_Kursovoy.Service
{
    class Command<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        Action<T> _execute;
        Func<T, bool> _canExecute;

        public Command(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null && (_canExecute == null || _canExecute((T)parameter));
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
