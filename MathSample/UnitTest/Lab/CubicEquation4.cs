using System;
using System.Linq;
using static System.Math;

namespace UnitTest.Lab
{
	// 重解を重複させる場合です。
	public static class CubicEquation4
	{
		public static double[] Solve(double c, double d)
		{
			if (d < 0) return Solve(c, -d).Reverse().Select(x => -x).ToArray();

			// 最小の実数解
			var x1 = d == 0 && c >= 0 ? 0 : SolveNegative();

			// f(x) = (x - x_1) (x^2 + x_1 x + x_1^2 + c)
			var det = -3 * x1 * x1 - 4 * c;
			if (det < 0) return new[] { x1 };
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
