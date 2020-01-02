using System;
using System.Collections.Generic;
using System.Reflection;
using CalculatorGUI.Models.Operations;

namespace CalculatorGUI.Models
{
	class Calculator
	{
		private readonly Dictionary<string, IOperation> _operationDictionary;
		private readonly Dictionary<string, double> _constants;
		private readonly Stack<double> _remembered;
		private readonly Stack<double> _numbers;
		private readonly Stack<string> _history;
		private readonly DegreeConverter _converter;

		public bool IsDegreesEnabled
		{
			get => _converter.IsDegreesEnabled;
			set => _converter.IsDegreesEnabled = value;
		}

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
				["sin"] = new Sin(_converter),
				["cos"] = new Cos(_converter),
				["tg"] = new Tan(_converter),
				["arcsin"] = new Sin(_converter),
				["arccos"] = new Cos(_converter),
				["arctg"] = new Tan(_converter)
			};

			_constants = new Dictionary<string, double>()
			{
				["pi"] = Math.PI,
				["e"] = Math.E
			};

			_remembered = new Stack<double>();
			_numbers = new Stack<double>();
			_history = new Stack<string>();

			_converter = new DegreeConverter()
			{
				IsDegreesEnabled = false
			};
		}

		public double Calculate(string expression)
		{
			_remembered.Pop();
			_numbers.Clear();

			expression = InfixToPostfix(expression);
			string parsed = "";

			for (int i = 0; i < expression.Length; i++)
			{
				while (expression[i] != ' ')
					parsed += expression[i++];

				if (_operationDictionary.TryGetValue(parsed, out IOperation operation))
				{
					operation.Operate(_numbers);
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

			_remembered.Push(_numbers.Peek());
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

		private Stack<string> History()
		{
			return _history;
		}

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
	}
}