using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace homework1
{
    internal class Array : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private double maxx, minn, avg, sum;
        private string list;
        private Visibility error;

        public Array()
        {
            maxx = minn =avg = sum = 0;
            list = string.Empty;
            error = Visibility.Collapsed;
        }

        private void ListHandle(string list)
        {
            string[] arr = list.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 0)
            {
                Sum = Avg = Maxx = Minn = 0;
                return;
            }
            double sum = 0;
            int count = 0;
            double maxx = double.MinValue, minn = double.MaxValue;
            foreach (string s in arr)
            {
                double num;
                bool ok = double.TryParse(s, out num);
                if (!ok)
                {
                    Error = Visibility.Visible;
                    return;
                }
                sum += num;
                maxx = Double.MaxNumber(maxx, num);
                minn = Double.MinNumber(minn, num);
                ++count;
            }

            Error = Visibility.Collapsed;
            Maxx = maxx;
            Minn = minn;
            Sum = sum;
            Avg = sum / count;
        }

        public Visibility Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        public string List
        {
            get => list;
            set
            {
                list = value;
                ListHandle(list);
            }
        }
        public double Maxx
        {
            get => maxx;
            set
            {
                if (Math.Abs(maxx - value) > 0.001)
                {
                    maxx = value;
                    OnPropertyChanged();
                }
            }

        }

        public double Minn
        {
            get => minn;
            set
            {
                if (Math.Abs(minn - value) > 0.001)
                {
                    minn = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Avg
        {
            get => avg;
            set
            {
                if (Math.Abs(avg - value) > 0.001)
                {
                    avg = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Sum
        {
            get => sum;
            set
            {
                if (Math.Abs(sum - value) > 0.001)
                {
                    sum = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
