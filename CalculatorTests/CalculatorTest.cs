using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorGUI.Models;

namespace CalculatorTests
{
	[TestClass]
	public class CalculatorTest
	{
		private static bool IsDoubleEquals(double l, double r)
		{
			return Math.Abs(l - r) < 0.0000001; //double.Epsilon * 2048 * 8;
		}

		[TestMethod]
		public void SimpleIntTest()
		{
			var calc = new Calculator();
			Assert.AreEqual(-94, calc.Calculate("20*2/4+17-11^2"));
		}

		[TestMethod]
		public void SimpleDoubleTest()
		{
			var calc = new Calculator();
			Assert.IsTrue(IsDoubleEquals(-13009.8406566087, 
				calc.Calculate("230.17/4.887+170.131-115.009^2")));
		}

		[TestMethod]
		public void FactorialTest()
		{
			var calc = new Calculator();
			Assert.AreEqual(720, calc.Calculate("3!!"));
		}

		[TestMethod]
		public void ExprFactorialTest()
		{
			var calc = new Calculator();
			Assert.AreEqual(39916806, calc.Calculate("(17-16+8/4)!+((11-5)*2-1)!"));
		}

		[TestMethod]
		public void UnaryMinusTest()
		{
			var calc = new Calculator();
			Assert.IsTrue(IsDoubleEquals(-17, calc.Calculate("-(26-9)")));
		}

		[TestMethod]
		public void ModTest()
		{
			var calc = new Calculator();
			Assert.AreEqual(1, calc.Calculate("15%2"));
			Assert.AreEqual(0, calc.Calculate("15%3"));
			Assert.AreEqual(6, calc.Calculate("90%7"));
		}

		[TestMethod]
		public void SqrtTest()
		{
			var calc = new Calculator();
			Assert.IsTrue(IsDoubleEquals(Math.Sqrt(2), calc.Calculate("sqrt(2)")));
			Assert.IsTrue(IsDoubleEquals(Math.Sqrt(18 * 19.0 / 77), calc.Calculate("sqrt(18 * 19 / 77)")));
			Assert.IsTrue(IsDoubleEquals(177, calc.Calculate("sqrt(177^2)")));
		}

		[TestMethod]
		public void TrigonometryTest()
		{
			var calc = new Calculator();
			Assert.IsTrue(IsDoubleEquals(1,
				calc.Calculate("sin(0) + sin(pi) + sin(pi/2)")));
			Assert.IsTrue(IsDoubleEquals(0,
				calc.Calculate("cos(0) + cos(pi) + cos(pi/2)")));
			Assert.IsTrue(IsDoubleEquals(1 + Math.Sqrt(3),
				calc.Calculate("tg(pi/4) + tg(pi/3)")));
			Assert.IsTrue(IsDoubleEquals(Math.PI / 2 + Math.PI / 6,
				calc.Calculate("arcsin(1) + arcsin(1/2)")));
			Assert.IsTrue(IsDoubleEquals(2 * Math.PI / 3,
				calc.Calculate("arccos(1) + arccos(-1/2)")));
			Assert.IsTrue(IsDoubleEquals(Math.PI / 4 - Math.PI / 3,
				calc.Calculate("arctg(1) + arctg(-sqrt(3))")));
		}

		[TestMethod]
		public void AbsTest()
		{
			var calc = new Calculator();
			Assert.AreEqual(64, calc.Calculate("abs(-2^3!)"));
			Assert.AreEqual(64, calc.Calculate("abs(2^7-2^6)"));
			Assert.AreEqual(64, calc.Calculate("abs(2^6-2^7)"));
		}
	}
}
