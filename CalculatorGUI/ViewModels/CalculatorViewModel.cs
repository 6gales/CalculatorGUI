﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CalculatorGUI.Models;

namespace CalculatorGUI.ViewModels
{
	class CalculatorViewModel : INotifyPropertyChanged
	{
		private readonly Calculator _calculator;

		public CalculatorViewModel()
		{
			_calculator = new Calculator();
		}

		public IEnumerable<string> History => _calculator.History;

		public IEnumerable<double> Memory => _calculator.Memory;

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
