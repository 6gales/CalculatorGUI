using System;
using System.Collections.Generic;

namespace CalculatorGUI.Models.Operations
{
	class Cos : IOperation
	{
		private readonly DegreeConverter _converter;

		public Cos(DegreeConverter converter)
		{
			this._converter = converter;
		}

		public OperationPriority GetPriority() => OperationPriority.UnaryPriority;

		public void Operate(Stack<double> numbers)
		{
			if (numbers.Count < 1)
				throw new Exception("Bad syntax");

			numbers.Push(Math.Cos(_converter.ConvertDegreesIfNeeded(numbers.Pop())));
		}
	}
}