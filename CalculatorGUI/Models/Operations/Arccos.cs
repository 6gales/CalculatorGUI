﻿using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Arccos : IOperation
	{
		public OperationPriority GetPriority() => OperationPriority.FunctionPriority;

		public void Operate(Stack<double> numbers, CalculationCulture currentCulture)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			var value = Math.Acos(numbers.Pop());
			if (currentCulture.UseDegrees)
			{
				value = CalculationUtils.DegreesToRad(value);
			}

			numbers.Push(value);
		}
	}
}