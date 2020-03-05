using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CalculatorGUI.Models.Operations;

namespace CalculatorGUI.Models
{
	public class Calculator : ICalculator
	{
		private readonly Dictionary<string, IOperation> _operationDictionary;
		private readonly Dictionary<string, double> _constants;
		private readonly Stack<double> _memory;
		private readonly Stack<double> _numbers;
		private readonly Stack<string> _history;
		private readonly HashSet<string> _unaryOps;

		public ICalculationCulture CurrentCulture { get; }
		public Calculator()
		{
			CurrentCulture = new CalculationCulture();

			_operationDictionary = new Dictionary<string, IOperation>()
			{
				["+"] = new Add(),
				["-"] = new Sub(),
				["*"] = new Mul(),
				["/"] = new Div(),
				["^"] = new Pow(),
				["!"] = new Factorial(),
				["%"] = new Mod(),
				["u-"] = new UnaryMinus(),
				["sqrt"] = new Sqrt(),
				["abs"] = new Abs(),
				["sin"] = new Sin(),
				["cos"] = new Cos(),
				["tg"] = new Tan(),
				["arcsin"] = new Arcsin(),
				["arccos"] = new Arccos(),
				["arctg"] = new Arctan()
			};

			_unaryOps = new HashSet<string>()
			{
				"-"
			};

			_constants = new Dictionary<string, double>()
			{
				["π"] = Math.PI,
				["pi"] = Math.PI,
				["e"] = Math.E
			};

			_memory = new Stack<double>();
			_numbers = new Stack<double>();
			_history = new Stack<string>();
		}

		public void ClearMemory() => _memory.Clear();

		public void Remember(double number) => _memory.Push(number);

		public bool TryCalculate(string expression, out double result)
		{
			if (string.IsNullOrEmpty(expression))
			{
				result = 0;
				return false;
			}
			return TryComputePostfix(InfixToPostfix(expression), out result);
		}

		public double Calculate(string expression)
		{
			if (string.IsNullOrEmpty(expression))
			{
				throw new Exception("Empty string");
			}
			if (TryComputePostfix(InfixToPostfix(expression), out var result))
			{
				return result;
			}

			throw new Exception("Bad syntax");
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
			var postfix = new StringBuilder();

			var operations = new Stack<string>();
			var operandBuilder = new StringBuilder();

			for (int i = 0; i < infix.Length; ++i)
			{
				if (char.IsWhiteSpace(infix[i]))
				{
					continue;
				}
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
						postfix.Append(operations.Pop()).Append(" ");
					}

					operations.Pop();
				}
				else
				{
					operandBuilder.Clear().Append(infix[i].ToString());
					if (char.IsLetterOrDigit(infix[i]))
					{
						while (++i < infix.Length && (char.IsLetterOrDigit(infix[i]) || infix[i] == '.'))
						{
							operandBuilder.Append(infix[i]);
						}
						i--;
					}

					var operand = operandBuilder.ToString();
					if (!_operationDictionary.TryGetValue(operand, out IOperation operation))
					{
						lastIsOp = false;
						postfix.Append(operand).Append(" ");
						continue;
					}

					if (_unaryOps.Contains(operand) && lastIsOp)
					{
						operand = "u" + operand;
					}

					while (operations.Count > 0
						&& _operationDictionary.TryGetValue(operations.Peek(), out var top)
						&& operation.GetPriority() <= top.GetPriority())
					{
						postfix.Append(operations.Pop()).Append(" ");
					}
					operations.Push(operand);

					lastIsOp = true;
				}

			}

			while (operations.Count > 0)
			{
				postfix.Append(operations.Pop()).Append(" ");
			}

			return postfix.ToString();
		}

		private bool TryComputePostfix(string expression, out double result)
		{
			_numbers.Clear();
			result = 0.0;
			var argBuilder = new StringBuilder();

			for (int i = 0; i < expression.Length; i++)
			{
				while (expression[i] != ' ')
				{
					argBuilder.Append(expression[i++]);
				}

				var parsed = argBuilder.ToString();
				if (_operationDictionary.TryGetValue(parsed, out var operation))
				{
					try
					{
						operation.Operate(_numbers, CurrentCulture);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						return false;
					}
				}
				else if ((double.TryParse(parsed, NumberStyles.Any,
					         CultureInfo.InvariantCulture, out var val))
				         || _constants.TryGetValue(parsed, out val))
				{
					_numbers.Push(val);
				}
				else
					return false;

				argBuilder.Clear();
			}

			_memory.Push(_numbers.Peek());
			result = _numbers.Pop();
			return true;
		}
		#endregion
	}
}