using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Arctan : IOperation
	{
		private readonly DegreeConverter _converter;

		public Arctan(DegreeConverter converter)
		{
			_converter = converter;
		}

		public OperationPriority GetPriority() => OperationPriority.UnaryPriority;

		public int NumberOfOperands()
		{
			return 1;
		}

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			numbers.Push(_converter.ConvertDegreesIfNeeded(Math.Atan(numbers.Pop())));
		}
	}
}