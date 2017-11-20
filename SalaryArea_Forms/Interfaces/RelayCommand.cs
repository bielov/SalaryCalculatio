using System;
using System.Windows.Input;

namespace SalaryArea_Forms.Interfaces
{
    public class RelayCommand :ICommand
    {
        private Action localAction;
        private bool _localCanExecute;
        public RelayCommand(Action action, bool canExecute)
        {
            localAction = action;
            _localCanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _localCanExecute;
        }

        public void Execute(object parameter)
        {
            localAction();
        }
    }
}
