using System;
using System.Linq;
using static System.Math;

namespace UnitTest
{
	public static class CubicEquation6
	{
		// (別解) 最小の実数解を求め、残りの二次方程式を解きます。
		public static double[] Solve(double c, double d)
		{
			var f = CubicEquation5.CreateFunction(c, d);
			var f1 = CubicEquation5.CreateDerivative(c);

			if (d < 0) return Solve(c, -d).Reverse().Select(x => -x).ToArray();
			// 3重解の場合 (c = d = 0)、f'(0) = 0 のため Newton 法を避けます。
			if (c == 0 && d == 0) return new[] { 0.0 };

			// 最小の実数解
			var x1 = SolveMin();
			// f(x) = (x - x_1) (x^2 + x_1 x + q)
			// q = x_1^2 + c;
			var det = -3 * x1 * x1 - 4 * c;
			if (det < 0) return new[] { x1 };
			if (det == 0) return new[] { x1, -x1 / 2 };
			return new[] { x1, (-x1 - Sqrt(det)) / 2, (-x1 + Sqrt(det)) / 2 };

			double SolveMin()
			{
				var x0 = -1.0;
				while (f(x0) > 0) x0 *= 2;
				return NewtonMethod.Solve(f, f1, x0);
			}
		}
	}
}
