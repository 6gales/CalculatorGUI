using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class PercentOf// : IOperation
	{
		public int GetPriority()
		{
			return 3;
		}

		public int NumberOfOperands()
		{
			return 2;
		}

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 2)
				throw new Exception("Bad syntax");

			double a = numbers.Pop(),
				b = numbers.Peek();

			numbers.Push(b * a / 100.0);
		}
	}
}