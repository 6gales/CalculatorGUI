using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Tan : IOperation
	{
		private readonly DegreeConverter _converter;

		public Tan(DegreeConverter converter)
		{
			_converter = converter;
		}

		public OperationPriority GetPriority() => OperationPriority.UnaryPriority;

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			numbers.Push(Math.Tan(_converter.ConvertDegreesIfNeeded(numbers.Pop())));
		}
	}
}