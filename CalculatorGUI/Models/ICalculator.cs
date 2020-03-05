namespace CalculatorGUI.Models
{
	public interface ICalculator
	{
		void ClearMemory();

		void Remember(double number);

		double Calculate(string expression);

		bool TryCalculate(string expression, out double result);

		ICalculationCulture CurrentCulture { get; }
	}
}
