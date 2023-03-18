using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace homework2
{
    internal class Click : ICommand, INotifyPropertyChanged
    {
        public event EventHandler? CanExecuteChanged;
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _result = String.Empty;
        private static readonly Random Random = new();

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            StringBuilder sb = new();
            double sum = 0;
            for (int i = 1; i <= 10; i++)
            {
                int num = Random.Next(0, 3);
                double area = ShapeFactor.CreateRandomShape((ShapeFactor.ShapeType)num).GetArea();
                sum += area;
                sb.AppendFormat("{2}: 创建了一个 {0}，面积为 {1}\n", ShapeFactor.ShapeName[num],
                    area, i);
            }
            sb.AppendFormat("总面积为 {0}\n", sum);
            Result = sb.ToString();
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
