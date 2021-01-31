using System;

namespace SimpleCalculator.Models
{
    public class CalculatorModel
    {
        private string _firstNumber;
        public string FirstNumber
    {
            get
            {
                return _firstNumber;
            }

            set
            {
                _firstNumber = value;
            }
        }

        private string _secondNumber;
        public string SecondNumber 
        { 
            get
            {
                return _secondNumber;
            }

            set
            {
                _secondNumber = value;
            }
        }

        private string _operation;
        public string Operation 
        { 
            get
            {
                return _operation;
            }

            set
            {
                _operation = value;
            }
        }

        private string _result;
        public string Result => _result;

        internal void CalculateResult()
        {
            switch(_operation)
            {
                case "+":
                    _result = (Convert.ToDouble(FirstNumber) + Convert.ToDouble(SecondNumber)).ToString();
                    break;

                case "-":
                    _result = (Convert.ToDouble(FirstNumber) - Convert.ToDouble(SecondNumber)).ToString();
                    break;

                case "x":
                    _result = (Convert.ToDouble(FirstNumber) * Convert.ToDouble(SecondNumber)).ToString();
                    break;

                case "/":
                    _result = (Convert.ToDouble(FirstNumber) / Convert.ToDouble(SecondNumber)).ToString();
                    break;

                case "%":
                    _result = (Convert.ToDouble(FirstNumber) / 100 ).ToString();
                    break;
            }
        }
    }
}
