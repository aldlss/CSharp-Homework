using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace homework4
{
    class TextMarge : INotifyPropertyChanged, ICommand
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CanExecuteChanged;
        private string _result;

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
            if (parameter is null)
                return false;
            var textSelects = (object[])parameter;
            if (textSelects.Length != 2)
                return false;
            return ((TextSelect)textSelects[0]).IsExist == Visibility.Hidden && ((TextSelect)textSelects[1]).IsExist == Visibility.Hidden;
        }

        public async void Execute(object? parameter)
        {
            
        }

        private async Task<string> DoTextMarge(string path1, string path2, string outFilePath)
        {
            try
            {
                var file1 = File.OpenText(path1);
                var file2 = File.OpenText(path2);
                var file1Text = await file1.ReadToEndAsync();
                var file2Text = await file2.ReadToEndAsync();

                var directoryPath = Path.GetDirectoryName(outFilePath);
                if (Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                var fileOut = File.CreateText(outFilePath);
                await fileOut.WriteLineAsync("文件 1 内容如下：");
                await fileOut.WriteAsync(file1Text);
                await fileOut.WriteLineAsync("\n文件 2 内容如下");
                await fileOut.WriteAsync(file2Text);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
