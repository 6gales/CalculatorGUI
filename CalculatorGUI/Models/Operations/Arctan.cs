using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Arctan : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.FunctionPriority;

		public int NumberOfOperands()
		{
			return 1;
		}

		public void Operate(Stack<double> numbers, CalculationCulture currentCulture)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			var value = Math.Atan(numbers.Pop());
			if (currentCulture.UseDegrees)
			{
				value = CalculationUtils.DegreesToRad(value);
			}

			numbers.Push(value);
		}
	}
}