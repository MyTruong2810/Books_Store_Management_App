using Catel.MVVM;
using System;
using System.Windows.Input;

namespace Books_Store_Management_App
{

    /// <summary>
    /// Lớp giúp cho việc xử lý UI và code-behind tách biệt nhau, mọi xử lý điều chuyển qua cho VỉewModel đảm nhiệm
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeWithParameter;
        private readonly Func<object, bool> _canExecute;
        private readonly Action _executeWithoutParameter;
        private ICommand changeToQRCodeCommand;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _executeWithParameter = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute, Func<object, bool> canExecute = null)
        {
            _executeWithoutParameter = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(ICommand changeToQRCodeCommand)
        {
            this.changeToQRCodeCommand = changeToQRCodeCommand;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_executeWithoutParameter != null)
            {
                _executeWithoutParameter();
            }
            else
            {
                _executeWithParameter(parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}