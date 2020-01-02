using System.Windows;
using System.Windows.Controls;

namespace CalculatorGUI.ViewModels
{
	class InversiveButton : Button
	{
		public void InverseContent()
		{
			string tmp = MainContent;
			MainContent = InversedContent;
			InversedContent = tmp;
			Content = MainContent;
		}

		public static readonly DependencyProperty MainProperty =
			DependencyProperty.Register("MainContent", typeof(string), typeof(InversiveButton));
		public static readonly DependencyProperty InversedProperty =
			DependencyProperty.Register("InversedContent", typeof(string), typeof(InversiveButton));
		
		public string MainContent
		{
			get => (string)GetValue(MainProperty);
			set => SetValue(MainProperty, value);
		}

		public string InversedContent
		{
			get => (string)GetValue(InversedProperty);
			set => SetValue(InversedProperty, value);
		}
	}
}
