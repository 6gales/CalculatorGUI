using System;
using System.Collections.Generic;
using System.Reflection;
using CalculatorGUI.Models.Operations;

namespace CalculatorGUI.Models
{
	class Calculator : ICalculator
	{
		private readonly Dictionary<string, IOperation> _operationDictionary;
		private readonly Dictionary<string, double> _constants;
		private readonly Stack<double> _memory;
		private readonly Stack<double> _numbers;
		private readonly Stack<string> _history;

		public CalculationCulture CurrentCulture { get; set; }
		public Calculator()
		{
			_operationDictionary = new Dictionary<string, IOperation>()
			{
				["+"] = new Add(),
				["-"] = new Sub(),
				["*"] = new Mul(),
				["/"] = new Div(),
				["^"] = new Pow(),
				["!"] = new Factorial(),
//				["%"] = new PercentOf(),
//				["u%"] = new OnePercent(),
				["u-"] = new UnaryMinus(),
				["sqrt"] = new Sqrt(),
				["sin"] = new Sin(),
				["cos"] = new Cos(),
				["tg"] = new Tan(),
				["arcsin"] = new Sin(),
				["arccos"] = new Cos(),
				["arctg"] = new Tan()
			};

			_constants = new Dictionary<string, double>()
			{
				["π"] = Math.PI,
				["e"] = Math.E
			};

			_memory = new Stack<double>();
			_numbers = new Stack<double>();
			_history = new Stack<string>();
		}

		public void ClearMemory() => _memory.Clear();

		public void Remember(double number) => _memory.Push(number);

		public double Calculate(string expression)
		{
			_memory.Pop();
			_numbers.Clear();

			expression = InfixToPostfix(expression);
			string parsed = "";

			for (int i = 0; i < expression.Length; i++)
			{
				while (expression[i] != ' ')
					parsed += expression[i++];

				if (_operationDictionary.TryGetValue(parsed, out IOperation operation))
				{
					operation.Operate(_numbers, CurrentCulture);
				}
				else
				{
					try
					{
						_numbers.Push(double.Parse(parsed));
					}
					catch (FormatException)
					{
						if (_constants.TryGetValue(parsed, out double val))
							_numbers.Push(val);
						else throw new Exception("Bad syntax");
					}
				}
				parsed = "";
			}

			_memory.Push(_numbers.Peek());
			return _numbers.Pop();
		}

		public double GetResult(string expression)
		{
			_history.Push(expression);
			return Calculate(expression);
		}

		public void WriteToHistory(string expression)
		{
			_history.Push(expression);
		}

		public IEnumerable<string> History => _history;

		public IEnumerable<double> Memory => _memory;

		#region Private
		private string InfixToPostfix(string infix)
		{
			bool lastIsOp = true;
			int braces = 0;
			string postfix = "";

			Stack<string> operations = new Stack<string>();

			for (int i = 0; i < infix.Length; ++i)
			{
				if (infix[i] == '(')
				{
					braces++;
					operations.Push(infix[i].ToString());
					lastIsOp = true;
				}
				else if (infix[i] == ')')
				{
					lastIsOp = true;
					braces--;
					if (braces < 0)
						throw new Exception("Bad syntax");

					while (operations.Count > 0 && operations.Peek() != "(")
					{
						postfix += operations.Pop() + " ";
					}

					operations.Pop();
				}
				else
				{
					string op = infix[i].ToString();
					if (char.IsLetterOrDigit(infix[i]))
					{
						while (++i < infix.Length && char.IsLetterOrDigit(infix[i]))
						{
							op += infix[i];
						}
						i--;
					}

					if (!_operationDictionary.TryGetValue(op, out IOperation operation))
					{
						lastIsOp = false;
						postfix += op + " ";
						continue;
					}

					if (op == "-" && lastIsOp)
					{
						op = "u-";
					}
					else if (op == "%")
					{
						if (operations.Count == 0
							|| operations.Peek() != "-"
							|| operations.Peek() != "+")
						{
							op = "u%";
						}
					}

					while (operations.Count > 0
						&& _operationDictionary.TryGetValue(operations.Peek(), out IOperation top)
						&& operation.GetPriority() <= top.GetPriority())
					{
						postfix += operations.Pop() + " ";
					}
					operations.Push(op);

					lastIsOp = true;
				}

			}

			while (operations.Count > 0)
			{
				postfix += operations.Pop() + " ";
			}

			return postfix;
		}
		#endregion
	}
}