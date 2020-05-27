using System;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class QuadraticEquationTest
	{
		readonly Func<double, double, double, double[]> target1 = QuadraticEquation1.Solve;
		readonly Func<double, double[]> target2 = QuadraticEquation1.Solve;

		[TestMethod]
		public void Solve_1()
		{
			void Test(double a, double b, double c)
			{
				var actual = target1(a, b, c);
				var det = b * b - 4 * a * c;
				// Errors can be occurred.
				Assert.AreEqual(det < 0 ? 0 : det == 0 ? 1 : 2, actual.Length);

				var f = QuadraticEquation1.CreateFunction(a, b, c);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x), -9);
			}

			// A case with error for c - b * b / 4.
			Test(100, 80, 16);

			for (int a = -50; a <= 50; a++)
				if (a != 0)
					for (int b = -50; b <= 50; b++)
						for (int c = -50; c <= 50; c++)
							Test(a, b, c);
		}

		[TestMethod]
		public void Solve_2()
		{
			void Test(double c)
			{
				var actual = target2(c);
				var det = -c;
				Assert.AreEqual(det < 0 ? 0 : det == 0 ? 1 : 2, actual.Length);

				var f = QuadraticEquation1.CreateFunction(c);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x), -9);
			}

			for (int c = -10000; c <= 10000; c++)
				Test(c);
		}
	}
}
