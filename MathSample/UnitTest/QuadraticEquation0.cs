using System;

namespace UnitTest
{
	public static class QuadraticEquation0
	{
		static Func<double, double> CreateFunction(double a, double b, double c) =>
			x => x * (a * x + b) + c;
		static Func<double, double> CreateDerivative(double a, double b) =>
			x => 2 * a * x + b;

		// f(x) = ax^2 + bx + c = 0
		public static double[] Solve(double a, double b, double c)
		{
			if (a == 0) throw new ArgumentException("The value must not be 0.", nameof(a));
			if (a < 0) return Solve(-a, -b, -c);

			var det = b * b - 4 * a * c;
			if (det < 0) return new double[0];
			if (det == 0) return new[] { -b / (2 * a) };

			var f = CreateFunction(a, b, c);
			var f1 = CreateDerivative(a, b);
			var x01 = (-b - Math.Max(det, 1)) / (2 * a);
			var x02 = (-b + Math.Max(det, 1)) / (2 * a);
			return new[] { NewtonMethod.Solve(f, f1, x01), NewtonMethod.Solve(f, f1, x02) };
		}
	}
}
