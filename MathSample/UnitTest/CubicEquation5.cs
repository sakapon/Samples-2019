using System;
using System.Linq;
using static System.Math;

namespace UnitTest
{
	public static class CubicEquation5
	{
		public static Func<double, double> CreateFunction(double a, double b, double c, double d) =>
			x => x * (x * (a * x + b) + c) + d;
		internal static Func<double, double> CreateFunction(double c, double d) =>
			x => x * (x * x + c) + d;
		internal static Func<double, double> CreateDerivative(double c) =>
			x => 3 * x * x + c;

		public static double[] Solve(double c, double d)
		{
			if (d < 0) return Solve(c, -d).Reverse().Select(x => -x).ToArray();
			// 3重解の場合 (c = d = 0) を含む
			if (d == 0) return c >= 0 ? new[] { 0D } : new[] { -Sqrt(-c), 0D, Sqrt(-c) };

			var det_m = -4 * c * c * c - 27 * d * d;
			// 重解の場合
			if (det_m == 0) return new[] { -2 * Sqrt(-c / 3), Sqrt(-c / 3) };

			var f = CreateFunction(c, d);
			var f1 = CreateDerivative(c);

			// 負の実数解
			var x1 = SolveNegative();
			if (det_m < 0) return new[] { x1 };

			// f(x) = (x - x_1) (x^2 + x_1 x + q)
			// q = c + x_1^2;
			var sqrt_det = Sqrt(-4 * c - 3 * x1 * x1);
			return new[] { x1, (-x1 - sqrt_det) / 2, (-x1 + sqrt_det) / 2 };

			double SolveNegative()
			{
				var x0 = -1D;
				while (f(x0) > 0) x0 *= 2;
				return NewtonMethod.Solve(f, f1, x0);
			}
		}
	}
}
