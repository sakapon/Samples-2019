using System;

namespace UnitTest
{
	public static class QuadraticEquation1
	{
		public static Func<double, double> CreateFunction(double a, double b, double c) =>
			x => x * (a * x + b) + c;
		internal static Func<double, double> CreateFunction(double c) =>
			x => x * x + c;
		internal static Func<double, double> CreateDerivative() =>
			x => 2 * x;

		// f(x) = ax^2 + bx + c = 0
		public static double[] Solve(double a, double b, double c) =>
			a != 0 ? Solve(b / a, c / a) : throw new ArgumentException("The value must not be 0.", nameof(a));

		// f(x) = x^2 + bx + c = 0
		public static double[] Solve(double b, double c) =>
			Array.ConvertAll(Solve(c - b * b / 4), x => x - b / 2);

		// f(x) = x^2 + c = 0
		public static double[] Solve(double c)
		{
			if (c > 0) return new double[0];
			if (c == 0) return new[] { 0D };

			var f = CreateFunction(c);
			var f1 = CreateDerivative();
			var x0 = Math.Max(-c, 1);
			var x1 = NewtonMethod.Solve(f, f1, x0);
			return new[] { -x1, x1 };
		}
	}
}
