using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Sqrt : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.FunctionPriority;

		public void Operate(Stack<double> numbers, ICalculationCulture currentCulture)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			numbers.Push(Math.Sqrt(numbers.Pop()));
		}
	}
}