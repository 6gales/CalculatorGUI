using System;

namespace CalculatorGUI.Models
{
	class DegreeConverter
	{
		public bool IsDegreesEnabled { get; set; }

		public double ConvertDegreesIfNeeded(double value)
		{
			return IsDegreesEnabled ? Math.PI * value / 180.0 : value;
		}
	}
}
