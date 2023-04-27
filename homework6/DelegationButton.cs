using System;
using System.Windows.Input;

namespace homework6
{
    internal class DelegationButton : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private event Action<object?> ExecuteEvent;
        private event Predicate<object?>? CanExecuteEvent;

        public DelegationButton(Action<object?> executeEvent, Predicate<object?>? canExecuteEvent = null)
        {
            ExecuteEvent = executeEvent;
            CanExecuteEvent = canExecuteEvent;
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecuteEvent is null || CanExecuteEvent.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            ExecuteEvent.Invoke(parameter);
        }
    }
}
