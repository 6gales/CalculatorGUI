using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	enum OperationPriority { SumPriority = 1, MulPriority, PowPriority, UnaryPriority, FunctionPriority }

	interface IOperation
	{
		OperationPriority GetPriority();

		void Operate(Stack<double> numbers, CalculationCulture currentCulture);
	}
}