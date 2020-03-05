using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Abs : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.FunctionPriority;

		public void Operate(Stack<double> numbers, ICalculationCulture currentCulture)
		{
			numbers.Push(Math.Abs(numbers.Pop()));
		}
	}
}
