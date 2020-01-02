using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Sub : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.SumPriority;

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 2)
				throw new Exception("Bad syntax");

			double a = numbers.Pop(),
				b = numbers.Pop();

			numbers.Push(b - a);
		}
	}
}