using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace homework1
{
    internal class Prime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private int up, down;
        private string ans;

        public Prime()
        {
            up = down = 0;
            ans = string.Empty;
        }

        private static bool IsPrime(int num)
        {
            if (num == 2) return true;
            if (num <= 1 || num % 2 == 0) return false;
            for (int i = 3; i <= Double.Sqrt(num); i += 2)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void PrimeSearch()
        {
            int conut = 0;
            StringBuilder sb = new();
            for (int i = (down <= 1 ? 2 : down); i <= up; i++)
            {
                if (IsPrime(i))
                {
                    sb.AppendFormat("{0}, ", i);
                    ++conut;
                    if (conut == 10)
                    {
                        sb.AppendLine("");
                        conut = 0;
                    }
                }
            }

            Ans = sb.ToString();
        }

        public string Ans
        {
            get => ans;
            set
            {
                if (ans != value)
                {
                    ans = value;
                    OnPropertyChanged();
                }
            }
        }


        public int Up
        {
            get => up;
            set
            {
                if (up != value)
                {
                    up = value;
                    PrimeSearch();
                }
            }
        }

        public int Down
        {
            get => down;
            set
            {
                if (down != value)
                {
                    down = value;
                    PrimeSearch();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
