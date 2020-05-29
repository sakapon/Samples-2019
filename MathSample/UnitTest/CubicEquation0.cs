using System;
using System.Linq;

namespace UnitTest
{
	// 一般形のまま Newton 法を適用します。
	// 誤差は小さいです。
	public static class CubicEquation0
	{
		static Func<double, double> CreateFunction(double a, double b, double c, double d) =>
			x => x * (x * (a * x + b) + c) + d;
		static Func<double, double> CreateDerivative(double a, double b, double c) =>
			x => x * (3 * a * x + 2 * b) + c;

		// f(x) = ax^3 + bx^2 + cx + d = 0
		public static double[] Solve(double a, double b, double c, double d)
		{
			if (a == 0) throw new ArgumentException("The value must not be 0.", nameof(a));
			if (a < 0) return Solve(-a, -b, -c, -d);

			var f = CreateFunction(a, b, c, d);
			var xc = -b / (3 * a);
			var yc = f(xc);
			if (yc < 0) return Solve(a, -b, c, -d).Reverse().Select(x => -x).ToArray();

			var f1 = CreateDerivative(a, b, c);
			// 自明解
			if (yc == 0 && f1(xc) >= 0) return new[] { xc };

			// xc より小さい実数解
			var x1 = SolveNegative();
			var p = a * x1 + b;
			var q = p * x1 + c;
			return QuadraticEquation0.Solve(a, p, q).Prepend(x1).ToArray();

			double SolveNegative()
			{
				var x0 = -1D;
				while (f(xc + x0) > 0) x0 *= 2;
				return NewtonMethod.Solve(f, f1, xc + x0);
			}
		}
	}
}
