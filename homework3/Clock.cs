using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace homework3
{
    internal class Clock : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CanExecuteChanged;

        public delegate void Tick(TimeSpan nowTimeSpan);
        public event Tick OnTick;

        public delegate void Alarm();
        public event Alarm OnAlarm;

        private readonly DispatcherTimer _timer;
        private TimeSpan _time;
        private string _timeString;
        private string _outputString;
        private readonly StringBuilder _sb;
        private Visibility _isErr;

        public Clock()
        {
            _timeString = string.Empty;
            _outputString = string.Empty;
            _time = TimeSpan.Zero;
            _sb = new StringBuilder();
            Time = TimeSpan.Zero;
            IsErr = Visibility.Hidden;
            OnTick += (time) =>
            {
                _sb.AppendFormat("滴答……还剩 {0} 秒响铃\n", time.TotalSeconds);
                OutputString = _sb.ToString();
            };
            OnAlarm += () =>
            {
                _sb.AppendLine("铃铃铃……时间到了");
                OutputString = _sb.ToString();
            };
            _timer = new()
            {
                Interval = TimeSpan.FromSeconds(1),
                IsEnabled = true
            };
            _timer.Stop();
            _timer.Tick += (_, _) =>
            {
                TimeSpan time = Time;
                if (time != TimeSpan.Zero)
                {
                    time -= TimeSpan.FromSeconds(1);
                    Time = time;
                    OnTick(time);
                }
                if (time == TimeSpan.Zero)
                {
                    OnAlarm();
                }
            };
        }

        public TimeSpan Time
        {
            get => _time;
            set
            {
                _time = value;
                TimeString = $"{value.Minutes:D2}:{value.Seconds:D2}";
            }
        }

        public string TimeString
        {
            get => _timeString;
            set
            {
                _timeString = value;
                OnPropertyChanged();
            }
        }

        public string OutputString
        {
            get => _outputString;
            set
            {
                _outputString = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is null)
            {
                IsErr = Visibility.Visible;
                return;
            }

            string? input = parameter.ToString();
            if (input is null)
            {
                IsErr = Visibility.Visible;
                return;
            }

            bool ok = int.TryParse(input, out int seconds);
            if (!ok)
            {
                IsErr = Visibility.Visible;
                return;
            }

            if (seconds <= 0 || seconds >= 3600) // 事实是已经通过输入限制了 seconds 不可能大于 3600 了
            {
                IsErr = Visibility.Visible;
                return;
            }

            OutputString = string.Empty;
            _sb.Clear();
            IsErr = Visibility.Hidden;
            _timer.Stop();
            Time = TimeSpan.FromSeconds(seconds);
            _timer.Start();
        }

        public Visibility IsErr
        {
            get => _isErr;
            set
            {
                _isErr = value;
                OnPropertyChanged();
            }
        }
    }
}
