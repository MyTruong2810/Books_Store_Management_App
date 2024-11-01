using Catel.MVVM;
using System;
using System.Windows.Input;

namespace Books_Store_Management_App
{
    //public class RelayCommand : ICommand
    //{
    //    private readonly Func<bool> _canExecute;
    //    private readonly Action _execute;

    //    public RelayCommand(Action execute, Func<bool> canExecute = null)
    //    {
    //        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    //        _canExecute = canExecute;
    //    }

    //    public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

    //    public void Execute(object parameter) => _execute();

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add => System.Windows.Input.CommandManager.RequerySuggested += value;
    //        remove => System.Windows.Input.CommandManager.RequerySuggested -= value;
    //    }
    //}

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        private ICommand changeToQRCodeCommand;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
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
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}