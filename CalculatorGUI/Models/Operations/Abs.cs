using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorGUI.Models.Operations
{
	class Abs : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.UnaryPriority;

		public void Operate(Stack<double> numbers)
		{
			numbers.Push(Math.Abs(numbers.Pop()));
		}
	}
}
