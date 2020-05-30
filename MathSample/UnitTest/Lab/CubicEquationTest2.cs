using System;
using System.Collections.Generic;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.Lab
{
	[TestClass]
	public class CubicEquationTest2
	{
		readonly Func<double, double, double, double[]> target1 = CubicEquation1.Solve;
		readonly Func<double, double, double[]> target2 = CubicEquation1.Solve;

		[TestMethod]
		public void Solve_1_1()
		{
			Test(new[] { 0.0 }, 1, 1, 0);
			Test(new[] { -2.0 }, 3, 4, 4);
			Test(new[] { -2.0 }, 1, -1, 2);
			Test(new[] { -3.0, 1.0 }, 1, -5, 3);

			void Test(double[] expected, double b, double c, double d)
			{
				var actual = target1(b, c, d);
				actual = Array.ConvertAll(actual, x => x.RoundAlmost());
				CollectionAssert.AreEqual(expected, actual);

				var f = CubicEquation1.CreateFunction(1, b, c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x));
			}
		}

		[TestMethod]
		public void Solve_1_2()
		{
			Test(3, 3, 3);
			Test(1, 0, 1);
			Test(1, -1, 0);
			Test(3, -2, -6);

			void Test(double b, double c, double d)
			{
				var actual = target1(b, c, d);

				var f = CubicEquation1.CreateFunction(1, b, c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x));
			}
		}

		[TestMethod]
		public void Solve_2_1()
		{
			Test(new[] { 0.0 }, 0, 0);
			Test(new[] { -1.0 }, 0, 1);
			Test(new[] { 2.0 }, 0, -8);
			Test(new[] { 0.0 }, 1, 0);
			Test(new[] { -2.0 }, 2, 12);
			Test(new[] { 1.0 }, 1, -2);
			Test(new[] { -3.0, 0.0, 3.0 }, -9, 0);
			Test(new[] { -3.0 }, -5, 12);
			Test(new[] { 5.0 }, -7, -90);
			Test(new[] { -4.0, 2.0 }, -12, 16);
			Test(new[] { -1.5, 3.0 }, -6.75, -6.75);
			Test(new[] { -3.0, 1.0, 2.0 }, -7, 6);
			Test(new[] { -7.0, -5.0, 12.0 }, -109, -420);

			void Test(double[] expected, double c, double d)
			{
				var actual = target2(c, d);
				CollectionAssert.AreEqual(expected, actual);

				var f = CubicEquation1.CreateFunction(c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x));
			}
		}

		[TestMethod]
		public void Solve_2_2()
		{
			Test(0, 2);
			Test(0, -100);
			Test(3, 0);
			Test(1, 1);
			Test(10, -10);
			Test(-7, 0);
			Test(-10, 127);
			Test(-1, -10);
			Test(-100, 90);
			Test(-15, -4);
			Test(-1.5, Math.Sqrt(2) / 2);
			Test(-6 * Math.Pow(2, 1 / 3.0), -8);

			void Test(double c, double d)
			{
				var actual = target2(c, d);
				var det = (-4 * c * c * c - 27 * d * d).RoundAlmost();
				Assert.AreEqual(c == 0 & d == 0 || det < 0 ? 1 : det == 0 ? 2 : 3, actual.Length);

				var f = CubicEquation1.CreateFunction(c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x));
			}
		}
	}
}
