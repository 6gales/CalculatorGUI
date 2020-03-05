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

				if (_calculator.TryCalculate(_userInput, out var result))
				{
					Result = result.ToString(CultureInfo.InvariantCulture);
				}
				else
				{
					Result = "";
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

		public ICalculationCulture CurrentCulture => _calculator.CurrentCulture;

		public IEnumerable<string> History => _calculator.History;
		public IEnumerable<double> Memory => _calculator.Memory;

		public void ClearMemory() => _calculator.ClearMemory();

		public void Remember()
		{
			if (double.TryParse(Result, out var result))
			{
				_calculator.WriteToHistory(UserInput);
				_calculator.Remember(result);
			}
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
