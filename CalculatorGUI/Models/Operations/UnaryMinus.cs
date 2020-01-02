using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class UnaryMinus : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.UnaryPriority;

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			numbers.Push(-numbers.Pop());
		}
	}
}