using System;
using System.Linq;
using static System.Math;

namespace UnitTest
{
	// 解の個数により場合分けします。
	// ここでは Math.Sqrt を使えることとします。
	public static class CubicEquation5
	{
		public static double[] Solve(double c, double d)
		{
			if (d < 0) return Solve(c, -d).Reverse().Select(x => -x).ToArray();
			// 3重解の場合 (c = d = 0) を含む
			if (d == 0) return c >= 0 ? new[] { 0D } : new[] { -Sqrt(-c), 0D, Sqrt(-c) };

			// この式では誤差が大きくなることがあります。
			var det3 = (-4 * c * c * c - 27 * d * d).RoundAlmost();
			// 重解の場合
			if (det3 == 0) return new[] { -2 * Sqrt(-c / 3), Sqrt(-c / 3) };

			// 負の実数解
			var x1 = SolveNegative();
			if (det3 < 0) return new[] { x1 };

			// f(x) = (x - x_1) (x^2 + x_1 x + x_1^2 + c)
			var sqrt_det2 = Sqrt(-3 * x1 * x1 - 4 * c);
			return new[] { x1, (-x1 - sqrt_det2) / 2, (-x1 + sqrt_det2) / 2 };

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
