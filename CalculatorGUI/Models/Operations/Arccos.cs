using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Arccos : IOperation
	{
		private readonly DegreeConverter _converter;

		public Arccos(DegreeConverter converter)
		{
			_converter = converter;
		}

		public OperationPriority GetPriority() => OperationPriority.UnaryPriority;

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			numbers.Push(_converter.ConvertDegreesIfNeeded(Math.Acos(numbers.Pop())));
		}
	}
}