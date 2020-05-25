using System;
using System.Collections.Generic;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class CubicEquationTest
	{
		readonly Func<double, double, double[]> target = CubicEquation5.Solve;

		[TestMethod]
		public void Solve_1()
		{
			void Test(double c, double d)
			{
				var actual = target(c, d);
				var f = CubicEquation5.CreateFunction(c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x));
			}

			for (int c = -100; c <= 100; c++)
				for (int d = 0; d <= 100; d++)
					Test(c, d);
		}

		[TestMethod]
		public void Solve_2()
		{
			void Test(double c, double d)
			{
				var actual = target(c, d);
				var f = CubicEquation5.CreateFunction(c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x));
			}

			Test(0, 2);
			Test(0, -100);
			Test(3, 0);
			Test(1, 1);
			Test(10, -10);
			Test(-7, 0);
			Test(-10, 127);
			Test(-1, -10);
			Test(-1.5, Math.Sqrt(2) / 2);
			Test(-6 * Math.Pow(2, 1 / 3.0), -8);
			Test(-100, 90);
			Test(-15, -4);
		}
	}
}
