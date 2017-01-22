using System;
using System.Windows.Input;

namespace Client.ViewModels.Commands
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;

        private bool _canExecute = true;

        public void SetCanExecute(bool val)
        {
            _canExecute = val;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute, bool canExecute = true)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
    }
}
