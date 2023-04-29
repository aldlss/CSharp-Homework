using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using AyaTools;
using homework8.dao;

namespace homework8.viewModel
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public string Translation
        {
            get => _translation;
            set
            {
                _translation = value;
                OnPropertyChanged();
            }
        }

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public string ResText
        {
            get => _resText;
            set
            {
                _resText = value;
                OnPropertyChanged();
            }
        }

        public Brush ResColor
        {
            get => _resColor;
            set
            {
                _resColor = value;
                OnPropertyChanged();
            }
        }

        public ICommand EnterCommand { get; private set; }

        private string _wordText, _translation, _inputText, _resText;
        private readonly WordsDb _wordsDb;
        private Brush _resColor;

        public ViewModel()
        {
            _wordText = _inputText = _translation = _resText = string.Empty;
            _resColor = Brushes.Black;
            var res = WordsDb.CreateWordsDbConnect(Environment.GetEnvironmentVariable("WORDS_DB_PATH")!);
            _wordsDb = res ?? throw new Exception("数据库连接失败");
            EnterCommand = new DelegationButton(EnterCheck);
            NextWord();
        }

        private void NextWord()
        {
            var aWord = _wordsDb.GetARandomWord();
            Translation = aWord.Translation;
            _wordText = aWord.WordText;
        }

        public void EnterCheck(object? _)
        {
            if (ResText == string.Empty)
            {
                if (InputText.Equals(_wordText))
                {
                    ResColor = Brushes.DarkSeaGreen;
                    ResText = "正确";
                }
                else
                {
                    ResColor = Brushes.OrangeRed;
                    ResText = $"错误，正确答案为 {_wordText}";
                }
            }
            else
            {
                ResText = string.Empty;
                InputText = string.Empty;
                NextWord();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
