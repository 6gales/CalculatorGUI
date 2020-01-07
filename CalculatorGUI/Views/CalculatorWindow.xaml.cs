using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CalculatorGUI.Models;
using CalculatorGUI.ViewModels;

namespace CalculatorGUI.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CalculatorWindow : Window
	{
		int cursorPosition = 0,
			selectionLength = 0;

		bool inv = false;

		private readonly List<InversiveButton> _inversiveButtons;

		private CalculatorViewModel _viewModel;

		public CalculatorWindow()
		{
			InitializeComponent();
			DataContext = _viewModel = new CalculatorViewModel();
			_inversiveButtons = new List<InversiveButton>()
			{
				SinButton,
				CosButton,
				TgButton
			};

			//	UserInput.SelectionChanged += txtSelectionChangeCommitted;
			//			this.PreviewTextInput += new TextCompositionEventHandler(TextInputEvent);
			//			TextInput a;
			//			this.OnTextInput;
			//this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
			//userInput.Content = Key.Subtract;
		}


		//		private void OnKeyDownHandler(object sender, KeyEventArgs e)
		//		{
		//			if (e.Key == Key.Return)
		//			{
		//				txtbx.Text = "You Entered: " + txtbx.Text;
		//			}
		//		}
		//		private void OnButtonKeyDown(object sender, KeyEventArgs e)
		//		{
		////			TextBox tb = new
		////			userInput.Content = userInput.Content + e.Key.ToString();
		//		}
		//		private void Button_Click(object sender, RoutedEventArgs e)
		//		{
		//			Button myButton = new Button();
		//			myButton.Width = 100;
		//			myButton.Height = 30;
		//			myButton.Content = "Кнопка";
		//			layoutGrid.Children.Add(myButton);
		//			string text = textBox1.Text;
		//			if (text != "")
		//			{
		//				MessageBox.Show(text);
		//			}
		//		}

		private void txtSelectionChangeCommitted(object sender, RoutedEventArgs e)
		{
			cursorPosition = UserInput.SelectionStart;
			selectionLength = UserInput.SelectionLength;
			//lbxEvents.Items.Add("cursor: " + cursorPosition + ", select: " + selectionLength);
		}

		protected int i = 0;
		private void KeyEvents(object sender, KeyEventArgs e)
		{
			cursorPosition = UserInput.SelectionStart;
			selectionLength = UserInput.SelectionLength;
			//lbxEvents.Items.Add("cursor: " + cursorPosition + ", select: " + selectionLength);
			//			if ((bool)chkIgnoreRepeat.IsChecked && e.IsRepeat) return;
			i++;
			string s = "Event" + i + ": " + e.RoutedEvent + " Клавиша: " + e.Key;
			//lbxEvents.Items.Add(s);
		}

		private void TxtContent_SelectionChanged(object sender, RoutedEventArgs e)
		{

		}

		private void TextInputEvent(object sender, TextCompositionEventArgs e)
		{
			i++;
			string s = "Event" + i + ": " + e.RoutedEvent + " Клавиша: " + e.Text;
			if (!UserInput.IsFocused)
				UserInput.Text += e.Text;
//			lbxEvents.Items.Add(s);


			cursorPosition = UserInput.SelectionStart;
			selectionLength = UserInput.SelectionLength;
//			lbxEvents.Items.Add("cursor: " + cursorPosition + ", select: " + selectionLength);
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
			if (sender is Button button)
			{
				UserInput.Text += button.Content;
			}
		}

		private void AddTextToInputWithBraceOnClick(object sender, RoutedEventArgs e)
		{
			if (sender is Button button)
			{
				UserInput.Text += button.Content + "(";
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
			UserInput.Text = "";
		}

		private void ClearMemoryOnClick(object sender, RoutedEventArgs e)
		{
//			_viewModel.
		}

		private void SetFromMemoryOnClick(object sender, RoutedEventArgs e)
		{
			UserInput.Text = $"{_viewModel.Memory.FirstOrDefault()}";
		}

		private void SaveToMemoryOnClick(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
