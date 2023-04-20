using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using homework5;

namespace homework6
{
    class ViewModel: INotifyPropertyChanged
    {
        private long? _nowNum, _preNum, _ansNum;
        private readonly char[] _operators = { '+', '-', '*', '\\' };
        private enum OperatorEnum
        {
            Plus,
            Minus,
            Multiple,
            Divide,
        }
        private OperatorEnum? _nowOperator;
        private string _displayText = string.Empty;

        public string DisplayText
        {
            get => _displayText;
            set
            {
                _displayText = value;
                OnPropertyChanged();
            }
        }

        public DelegationButton[] NumberButton { get; } = new DelegationButton[10];

        public DelegationButton[] OperatorButton { get; } = new DelegationButton[4];

        public DelegationButton ResetButton { get; }

        public DelegationButton GetSumButton { get; }

        public ViewModel()
        {
            Action<object?> NumberFuc(int i) => _ =>
            {
                if (_preNum is not null && _nowOperator is null)
                    _preNum = null;
                _nowNum ??= 0;
                _nowNum = _nowNum * 10 + i;
                ChangeDisplayText();
            };
            for (int i = 0; i < 10; ++i)
            {
                NumberButton[i] = new DelegationButton(NumberFuc(i));
            }

            Action<object?> OperatorFuc(OperatorEnum i) => _ =>
            {
                int opCode = 0;
                if (_nowOperator is not null)
                    opCode |= 0b0001;
                if (_nowNum is not null)
                    opCode |= 0b0010;
                if (_preNum is not null)
                    opCode |= 0b0100;
                switch (opCode)
                {
                    case 0b000:
                        break;
                    case 0b101:
                        _nowOperator = i;
                        break;
                    case 0b010:
                        _nowOperator = i;
                        _preNum = _nowNum;
                        _nowNum = null;
                        break;
                    case 0b111:
                        _preNum = CalSum(_preNum, _nowOperator, _nowNum);
                        if (_preNum is not null)
                            _nowOperator = i;
                        else _nowOperator = null;
                        _nowNum = null;
                        _ansNum = null;
                        break;
                    case 0b100:
                        _nowOperator = i;
                        break;
                    default:
                        Debug.WriteLine("jr2");
                        break;
                }
                ChangeDisplayText();
            };
            for (int i = 0; i < 4; i++)
            {
                OperatorButton[i] = new(OperatorFuc((OperatorEnum)i));
            }

            GetSumButton = new DelegationButton(_ =>
            {
                if (_nowNum is not null && _preNum is not null && _nowOperator is not null)
                {
                    _ansNum = CalSum(_preNum, _nowOperator, _nowNum);
                    ChangeDisplayText();
                }
            });

            ResetButton = new(_ =>
            {
                Reset();
                ChangeDisplayText();
            });
        }

        private static long? CalSum(long? a, OperatorEnum? operatorEnum, long? b)
        {
            // 应该是不可能的
            if (a is null || operatorEnum is null || b is null)
            {
                Debug.WriteLine("jr1");
                return null;
            }
            switch (operatorEnum)
            {
                case OperatorEnum.Plus:
                    return a + b;
                case OperatorEnum.Minus:
                    return a - b;
                case OperatorEnum.Multiple:
                    return a * b;
                case OperatorEnum.Divide:
                    return b == 0 ? null : a / b;
                default:
                    Debug.WriteLine("jr3");
                    return null;
            }
        }

        private void Reset()
        {
            _nowNum = null;
            _preNum = null;
            _nowOperator = null;
        }

        private void ChangeDisplayText()
        {
            DisplayText = string.Format($"{_preNum?.ToString() ?? string.Empty}" +
                                        $"{(_nowOperator is not null ? _operators[(int)_nowOperator!].ToString() : string.Empty)}" +
                                        $"{_nowNum?.ToString() ?? string.Empty}" +
                                        $"{(_ansNum is not null ? $"={_ansNum}" : string.Empty)}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
