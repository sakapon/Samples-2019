using System;
using System.Linq;
using static System.Math;

namespace UnitTest
{
	// 負の実数解を求め、残りの 2 次方程式を解きます。
	// ここでは Math.Sqrt を使えることとします。
	public static class CubicEquation6
	{
		public static double[] Solve(double c, double d)
		{
			if (d < 0) return Solve(c, -d).Reverse().Select(x => -x).ToArray();
			// 自明解
			if (d == 0 && c >= 0) return new[] { 0D };

			// 負の実数解
			var x1 = SolveNegative();
			// f(x) = (x - x_1) (x^2 + x_1 x + c + x_1^2)
			var det = -4 * c - 3 * x1 * x1;
			if (det < 0) return new[] { x1 };
			if (det == 0) return new[] { x1, -x1 / 2 };
			return new[] { x1, (-x1 - Sqrt(det)) / 2, (-x1 + Sqrt(det)) / 2 };

			double SolveNegative()
			{
				var f = CubicEquation1.CreateFunction(c, d);
				var f1 = CubicEquation1.CreateDerivative(c);
				var x0 = -1D;
				while (f(x0) > 0) x0 *= 2;
				return NewtonMethod.Solve(f, f1, x0);
			}
		}
	}
}
