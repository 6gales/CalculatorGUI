using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Factorial : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.UnaryPriority;//4?

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			var operand = numbers.Pop();
			var rounded = Math.Floor(operand);

			if (rounded != operand)
				throw new Exception("Factorial operand is not integer");

			int factorial = (int)rounded;
			for (int i = factorial - 1; i > 1; i--)
			{
				factorial *= i;
			}

			numbers.Push(factorial);
		}
	}
}