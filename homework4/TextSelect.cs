using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace homework4
{
    internal class TextSelect : INotifyPropertyChanged, ICommand
    {
        private string _path;
        private Visibility _isExist;

        // 定义 Path 变更事件
        public Action<string>? PathChanged;

        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                { 
                    _path = value;
                    OnPropertyChanged();
                    IsExist = File.Exists(_path) ? Visibility.Hidden : Visibility.Visible;
                    PathChanged?.Invoke(_path);
                }
            }
        }

        public Visibility IsExist
        {
            get => _isExist;
            set
            {
                if (_isExist != value)
                {
                    _isExist = value;
                    OnPropertyChanged();
                }
            }
        }

        public TextSelect()
        {
            _path = string.Empty;
            _isExist = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                ShowReadOnly = true,
            };
            if (dialog.ShowDialog() == true)
            {
                Path = dialog.FileName;
            }
        }
    }
}
