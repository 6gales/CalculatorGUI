using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Pow : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.PowPriority;

		public void Operate(Stack<double> numbers, ICalculationCulture currentCulture)
		{
			if (numbers.Count < 2)
				throw new Exception("Bad syntax");

			double a = numbers.Pop(),
				b = numbers.Pop();

			numbers.Push(Math.Pow(b, a));
		}
	}
}