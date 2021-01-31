using SimpleCalculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleCalculator.ViewModels
{
    public class CalculatorViewModel : CalculatorViewModelBase
    {
        private CalculatorModel _calculator;
        private ICommand _digitButtonClickCommand;
        private ICommand _operationButtonClickCommand;
        private List<string> _history;
        private string _display;
        private bool _clearDisplay = false;

        public ICommand DigitButtonClickCommand
        {
            get
            {
                return _digitButtonClickCommand;
            }

            set
            {
                if (_digitButtonClickCommand != value)
                {
                    _digitButtonClickCommand = value;
                }
            }
        }

        public ICommand OperationButtonClickCommand
        {
            get
            {
                return _operationButtonClickCommand;
            }

            set
            {
                if (_operationButtonClickCommand != value)
                {
                    _operationButtonClickCommand = value;
                }
            }
        }

        public string FirstNumber
        {
            get
            {
                return _calculator.FirstNumber;
            }
            set
            {
                _calculator.FirstNumber = value;
            }
        }

        public string SecondNumber
        {
            get
            {
                return _calculator.SecondNumber;
            }
            set
            {
                _calculator.SecondNumber = value;
            }
        }

        public string _operator;
        
        public string Operator
        {
            get
            {
                return _calculator.Operation;
            }
            set
            {
                _calculator.Operation = value;
            }
        }

        public string _previousOperator;
        public string PreviousOperator
        {
            get
            {
                return _previousOperator;
            }
            set
            {
                _previousOperator = value;
            }
        }

        public string Display
        {
            get
            {
                return _display;
            }

            set
            {
                if (_display != value)
                {
                    _display = value;
                    OnPropertyChanged("Display");
                }

            }
        }

        public List<string> History
        {
            get
            {
                return _history;
            }
            set
            {
                _history = value;
            }
        }

        public string Result => _calculator.Result;

        public CalculatorViewModel()
        {
            _calculator = new CalculatorModel();
            _history = new List<string>();
            Initialize();
            DigitButtonClickCommand = new RelayCommand<string>(DigitButtonClick);
            OperationButtonClickCommand = new RelayCommand<string>(OperationButtonClick);
        }

        private void Initialize()
        {
            _previousOperator = string.Empty;
            Display = "0";
            FirstNumber = string.Empty;
            SecondNumber = string.Empty;
        }

        private void OperationButtonClick(string @operator)
        {
            if (@operator == "%")
            {
                Operator = @operator;
                FirstNumber = _display;
                _calculator.CalculateResult();
                Display = Result;
                History.Add($"{Math.Round(Convert.ToDouble(FirstNumber), 10)}{Operator}={Math.Round(Convert.ToDouble(Result), 10)}");

                return;
            }

            if (FirstNumber == string.Empty || PreviousOperator == "=")
            {
                FirstNumber = _display;
                PreviousOperator = @operator;
            }
            else
            {
                SecondNumber = _display;
                Operator = _previousOperator;
                _calculator.CalculateResult();

                History.Add($"{Math.Round(Convert.ToDouble(FirstNumber), 10)}{Operator}={Math.Round(Convert.ToDouble(Result), 10)}");

                PreviousOperator = @operator;
                Display = Result;
                FirstNumber = _display;
            }
            _clearDisplay = true;
        }

        private void DigitButtonClick(string button)
        {
            switch (button)
            {
                case "C":
                    Initialize();
                    break;
                case "H":
                    if (_history == null)
                        Display = "There is no history yet";
                    else
                        Display = string.Join(Environment.NewLine, History.Take(5));
                    break;
                case ",":
                    if (_clearDisplay)
                    {
                        Display = "0,";
                    }
                    else
                    {
                        if (!_display.Contains(","))
                        {
                            Display = $"{_display},";
                        }
                    }
                    break;
                default:
                    if (_display == "0" || _clearDisplay)
                        Display = button;
                    else
                        Display = $"{_display}{button}";
                    break;
            }
        }
    }
}
