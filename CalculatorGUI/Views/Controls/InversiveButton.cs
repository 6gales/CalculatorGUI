using System.Windows;
using System.Windows.Controls;

namespace CalculatorGUI.Views.Controls
{
	class InversiveButton : Button
	{
		public void InverseContent()
		{
			var tmp = Content as string;
			Content = InversedContent;
			InversedContent = tmp;
		}
		
		public static readonly DependencyProperty InversedProperty =
			DependencyProperty.Register("InversedContent", typeof(string), typeof(InversiveButton));

		public string InversedContent
		{
			get => (string)GetValue(InversedProperty);
			set => SetValue(InversedProperty, value);
		}
	}
}
