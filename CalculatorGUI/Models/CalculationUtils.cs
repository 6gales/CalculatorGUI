using System;

namespace CalculatorGUI.Models
{
	static class CalculationUtils
	{
		public static double DegreesToRad(double value)
		{
			return Math.PI * value / 180.0;
		}
	}
}
