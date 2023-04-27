using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AyaTools
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

    internal class AsyncDelegationButton : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private event Func<object?, Task> ExecuteEvent;
        private event Predicate<object?>? CanExecuteEvent;
        private readonly TimeSpan _waitTime;

        public AsyncDelegationButton(Func<object?, Task> executeEvent, TimeSpan waitTime , Predicate<object?>? canExecuteEvent = null)
        {
            _waitTime = waitTime;
            ExecuteEvent = executeEvent;
            CanExecuteEvent = canExecuteEvent;
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecuteEvent is null || CanExecuteEvent.Invoke(parameter);
        }

        public async void Execute(object? parameter)
        {
            await ExecuteEvent(parameter).WaitAsync(_waitTime);
        }
    }
}
