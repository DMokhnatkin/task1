using System;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute)
        {
            _execute = execute;
        }
    }
}
