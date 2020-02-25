﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CalculatorGUI.Views.Controls;

namespace CalculatorGUI.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CalculatorWindow : Window
	{
		//private int _cursorPosition = 0;
		//private int _selectionStart = 0;
		//private int _selectionLength = 0;

		private readonly List<InversiveButton> _inversiveButtons;

		public CalculatorWindow()
		{
			InitializeComponent();
			_inversiveButtons = new List<InversiveButton>()
			{
				SinButton,
				CosButton,
				TgButton
			};
		}

		private void OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
				
					break;
				case Key.Back:
					UserInput.Text = UserInput.Text.Remove(UserInput.Text.Length - 1);
					break;
			}
		}

		private void HandleSelectionChanged(object sender, RoutedEventArgs e)
		{
			//_selectionStart = UserInput.SelectionStart;
			//_selectionLength = UserInput.SelectionLength;
			//_cursorPosition = UserInput.CaretIndex;
		}

		private void HandleTextInputEvent(object sender, TextCompositionEventArgs e)
		{
			if (!UserInput.IsFocused)
				CalculatorViewModel.UserInput += e.Text;
		}

		private void InverseButtonsOnClick(object sender, RoutedEventArgs e)
		{
			foreach (var inversiveButton in _inversiveButtons)
			{
				inversiveButton.InverseContent();
			}
		}

		private void InverseDegreesOnClick(object sender, RoutedEventArgs e)
		{
			if (sender is InversiveButton inversiveButton)
			{
				inversiveButton.InverseContent();
//				_calculator.IsDegreesEnabled = !_calculator.IsDegreesEnabled;
			}
		}

		private void AddTextToInputOnClick(object sender, RoutedEventArgs e)
		{
			var input = UserInput.Text.Remove(UserInput.SelectionStart, UserInput.SelectionLength);
			if (sender is Button button && button.Content is string content)
			{
				UserInput.Text = input.Insert(UserInput.CaretIndex, content);

				//UserInput.Text += button.Content;
			}
		}

		private void AddTextToInputWithBraceOnClick(object sender, RoutedEventArgs e)
		{
			var input = UserInput.Text.Remove(UserInput.SelectionStart, UserInput.SelectionLength);
			if (sender is Button button && button.Content is string content)
			{
				UserInput.Text = input.Insert(UserInput.CaretIndex, content + "(");
				//UserInput.Text += button.Content + "(";
			}
		}

		private void SetTextOnClick(object sender, RoutedEventArgs e)
		{
			if (sender is ListView list && list.SelectedItem is string newContent)
			{
				UserInput.Text = newContent;
			}
		}

		private void EraseSymbolOnClick(object sender, RoutedEventArgs e)
		{
			UserInput.Text = UserInput.Text.Remove(UserInput.Text.Length - 1);
		}

		private void ClearInputOnClick(object sender, RoutedEventArgs e)
		{
			CalculatorViewModel.UserInput = "";
		}

		private void ClearMemoryOnClick(object sender, RoutedEventArgs e)
		{
//			_viewModel.
		}

		private void SetFromMemoryOnClick(object sender, RoutedEventArgs e)
		{
			UserInput.Text = $"{CalculatorViewModel.Memory.FirstOrDefault()}";
		}

		private void SaveToMemoryOnClick(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
