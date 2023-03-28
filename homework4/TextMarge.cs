using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace homework4
{
    class TextMarge : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CanExecuteChanged;
        private string _result;
        private bool _running;
        private event Func<object?, Task> ExecuteEvent;
        private event Predicate<object?>? CanExecuteEvent;

        public TextMarge(Func<object?, Task> executeEvent, Predicate<object?>? canExecuteEvent = null)
        {
            ExecuteEvent = executeEvent;
            CanExecuteEvent = canExecuteEvent;
            _result = string.Empty;
            _running = false;
        }

        public string Result
        {
            get => _result;
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CanExecute(object? parameter)
        {
            return !_running && (CanExecuteEvent is null || CanExecuteEvent.Invoke(parameter));
        }

        public async void Execute(object? parameter)
        {
            _running = true;
            OnCanExecuteChanged();
            try
            {
                await ExecuteEvent(parameter).WaitAsync(TimeSpan.FromSeconds(10));
            }
            catch (TimeoutException)
            {
                Result = "合并失败，10 秒后仍无结果";
            }
            finally
            {
                _running = false;
                OnCanExecuteChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
