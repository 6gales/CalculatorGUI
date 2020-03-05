using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Sin : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.FunctionPriority;

		public void Operate(Stack<double> numbers, ICalculationCulture currentCulture)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			var value = numbers.Pop();
			if (currentCulture.UseDegrees)
			{
				value = CalculationUtils.DegreesToRad(value);
			}

			numbers.Push(Math.Sin(value));
		}
	}
}