using System;
using System.Linq;

namespace UnitTest
{
	// 負の実数解を求め、残りの二次方程式を解きます。
	public static class CubicEquation1
	{
		public static Func<double, double> CreateFunction(double a, double b, double c, double d) =>
			x => x * (x * (a * x + b) + c) + d;
		internal static Func<double, double> CreateFunction(double c, double d) =>
			x => x * (x * x + c) + d;
		internal static Func<double, double> CreateDerivative(double c) =>
			x => 3 * x * x + c;

		// f(x) = ax^3 + bx^2 + cx + d = 0
		public static double[] Solve(double a, double b, double c, double d) =>
			a != 0 ? Solve(b / a, c / a, d / a) : throw new ArgumentException("The value must not be 0.", nameof(a));

		// f(x) = x^3 + bx^2 + cx + d = 0
		public static double[] Solve(double b, double c, double d) =>
			Array.ConvertAll(Solve((c - b * b / 3).RoundAlmost(), (d - b * c / 3 + 2 * b * b * b / 27).RoundAlmost()), x => x - b / 3);

		// f(x) = x^3 + cx + d = 0
		public static double[] Solve(double c, double d)
		{
			if (d < 0) return Solve(c, -d).Reverse().Select(x => -x).ToArray();
			// 自明解
			if (d == 0 && c >= 0) return new[] { 0D };

			// 負の実数解
			var x1 = SolveNegative();
			// f(x) = (x - x_1) (x^2 + x_1 x + c + x_1^2)
			return QuadraticEquation0.Solve(1, x1, c + x1 * x1).Prepend(x1).ToArray();

			double SolveNegative()
			{
				var f = CreateFunction(c, d);
				var f1 = CreateDerivative(c);
				var x0 = -1D;
				while (f(x0) > 0) x0 *= 2;
				return NewtonMethod.Solve(f, f1, x0);
			}
		}
	}
}
