using System;

namespace UnitTest
{
	public static class QuadraticEquation1
	{
		public static Func<double, double> CreateFunction(double a, double b, double c) =>
			x => x  * (a * x + b)  + c;
		internal static Func<double, double> CreateFunction(double c) =>
			x => x * x + c;
		internal static Func<double, double> CreateDerivative() =>
			x => 2 * x ;

	}
}
