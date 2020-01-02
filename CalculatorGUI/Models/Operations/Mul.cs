using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Mul : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.MulPriority;

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 2)
				throw new Exception("Bad syntax");

			numbers.Push(numbers.Pop() * numbers.Pop());
		}
	}
}