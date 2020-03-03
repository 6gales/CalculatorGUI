namespace CalculatorGUI.Models
{
	interface ICalculator
	{
		void ClearMemory();

		void Remember(double number);

		double Calculate(string expression);
	}
}
