using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using CalculatorGUI.Models;

namespace CalculatorGUI.ViewModels
{
	class CalculatorViewModel : INotifyPropertyChanged
	{
		private readonly Calculator _calculator = new Calculator();
		private string _userInput;
		private string _result;

		public string UserInput
		{
			get => _userInput;
			set
			{
				_userInput = value;
				OnPropertyChanged(nameof(UserInput));

				try
				{
					var result = _calculator.Calculate(_userInput);
					Result = result.ToString(CultureInfo.CurrentCulture);
				}
				catch
				{
					// ignored
				}
			}
		}

		public string Result
		{
			get => _result;
			set
			{
				_result = value;
				OnPropertyChanged(nameof(Result));
			}
		}


		public IEnumerable<string> History => _calculator.History;
		public IEnumerable<double> Memory => _calculator.Memory;

		public void ClearMemory() => _calculator.ClearMemory();

		public void Remember(string expr)
		{
			if (double.TryParse(expr, out double result))
			{
				_calculator.Remember(result);
				return;
			}

			result = _calculator.Calculate(expr);
			_calculator.Remember(result);
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
