using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class OnePercent// : IOperation
	{
		public int GetPriority()
		{
			return 3;
		}

		public int NumberOfOperands()
		{
			return 1;
		}

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			numbers.Push(numbers.Pop() / 100.0);
		}
	}
}