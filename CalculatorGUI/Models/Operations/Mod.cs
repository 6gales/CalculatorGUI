using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Mod : IOperation
	{
		public void Operate(Stack<double> numbers, CalculationCulture currentCulture)
		{
			if (numbers.Count < 2)
				throw new Exception("Bad syntax");

			double a = numbers.Pop(),
				b = numbers.Peek();

			numbers.Push(b % a);
		}
		
		public OperationPriority GetPriority()
		{
			return OperationPriority.MulPriority;
		}
	}
}