using System;
using System.IO;
using System.Threading;
using System.Windows;

namespace homework4
{
    internal class ViewModel
    {
        private readonly TextSelect _textSelect1;
        private readonly TextSelect _textSelect2;
        private readonly TextMarge _textMarge0;

        public TextSelect TextSelect1 => _textSelect1;
        public TextSelect TextSelect2 => _textSelect2;
        public TextMarge TextMarge => _textMarge0;

        public ViewModel()
        {
            _textMarge0 = new TextMarge(async parameter => {
                _textMarge0.Result = "正在合并...";

                var path1 = _textSelect1.Path;
                var path2 = _textSelect2.Path;

                if (path1 == string.Empty || path2 == string.Empty)
                {
                    _textMarge0.Result = "路径为空";
                    return;
                }

                var nowPath = Environment.ProcessPath;
                if (nowPath is null)
                {
                    _textMarge0.Result = "获取当前路径失败";
                    return;
                }
                var outFilePath = Path.Combine(Path.GetDirectoryName(nowPath)!, "data", "out.txt");

                var textMarge = async () =>
                {
                    try
                    {
                        var file1 = File.OpenText(path1);
                        var file2 = File.OpenText(path2);
                        var file1Text = await file1.ReadToEndAsync();
                        var file2Text = await file2.ReadToEndAsync();

                        var directoryPath = Path.GetDirectoryName(outFilePath);
                        if (!Directory.Exists(directoryPath))
                            Directory.CreateDirectory(directoryPath);

                        var fileOut = File.CreateText(outFilePath);
                        fileOut.WriteLine("文件 1 内容如下：");
                        await fileOut.WriteAsync(file1Text).WaitAsync(CancellationToken.None);
                        fileOut.WriteLine("\n\n文件 2 内容如下：");
                        await fileOut.WriteAsync(file2Text).WaitAsync(CancellationToken.None);

                        fileOut.Close();

                        return "合并成功";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                };
                _textMarge0.Result = await textMarge();
            },_ => _textSelect1.IsExist == Visibility.Hidden && _textSelect2.IsExist == Visibility.Hidden);


            void PathChange(string _) => _textMarge0.OnCanExecuteChanged();

            _textSelect1 = new TextSelect()
            {
                PathChanged = PathChange,
            };

            _textSelect2 = new TextSelect()
            {
                PathChanged = PathChange,
            };
        }
    }
}
