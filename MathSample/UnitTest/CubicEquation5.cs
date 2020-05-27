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
			// Center: (0, d)
			var f = CreateFunction(c, d);
			var f1 = CreateDerivative(c);

			if (d < 0) return Solve(c, -d).Reverse().Select(x => -x).ToArray();
			// 3重解の場合 (c = d = 0) を含む
			if (d == 0) return c >= 0 ? new[] { 0.0 } : new[] { -Sqrt(-c), 0.0, Sqrt(-c) };

			// f が極値を持たない場合
			if (c >= 0) return new[] { SolveNegative() };

			// 以下、f が極値を持つ場合
			var det_m = 4 * c * c * c + 27 * d * d;
			if (det_m == 0) return new[] { -2 * Sqrt(-c / 3), Sqrt(-c / 3) };
			if (det_m > 0) return new[] { SolveNegative() };

			// 負の実数解
			var x1 = SolveNegative();
			// f(x) = (x - x_1) (x^2 + x_1 x + q)
			// q = x_1^2 + c;
			var sqrt_det = Sqrt(-3 * x1 * x1 - 4 * c);
			return new[] { x1, (-x1 - sqrt_det) / 2, (-x1 + sqrt_det) / 2 };

			double SolveNegative()
			{
				var x0 = -1.0;
				while (f(x0) > 0) x0 *= 2;
				return NewtonMethod.Solve(f, f1, x0);
			}
		}
	}
}
