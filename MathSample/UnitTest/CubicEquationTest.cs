using System;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class CubicEquationTest
	{
		readonly Func<double, double, double, double, double[]> target1 = CubicEquation5.Solve;
		readonly Func<double, double, double[]> target2 = CubicEquation5.Solve;

		[TestMethod]
		public void Solve_1()
		{
			void Test(double a, double b, double c, double d)
			{
				var actual = target1(a, b, c, d);

				var f = CubicEquation5.CreateFunction(a, b, c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x), -9);
			}

			// A case with error for the determinant.
			Test(20, 8, 0, 0);
			//Test(6, 47, 0, 0);
			//Test(5, 33, 0, 0);
			//Test(2, 49, 0, 0);

			for (int a = -15; a <= 15; a++)
				if (a != 0)
					for (int b = -15; b <= 15; b++)
						for (int c = -15; c <= 15; c++)
							for (int d = -15; d <= 15; d++)
								Test(a, b, c, d);
		}

		[TestMethod]
		public void Solve_2()
		{
			void Test(double c, double d)
			{
				var actual = target2(c, d);
				var det = -4 * c * c * c - 27 * d * d;
				Assert.AreEqual(c == 0 & d == 0 || det < 0 ? 1 : det == 0 ? 2 : 3, actual.Length);

				var f = CubicEquation5.CreateFunction(c, d);
				foreach (var x in actual)
					Assert2.AreNearlyEqual(0, f(x));
			}

			for (int c = -100; c <= 100; c++)
				for (int d = 0; d <= 100; d++)
					Test(c, d);
		}
	}
}
