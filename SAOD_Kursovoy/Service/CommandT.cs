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
            try
            {
                _execute((T)parameter);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка!",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}
