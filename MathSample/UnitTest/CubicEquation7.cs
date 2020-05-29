using System;
using System.Linq;

namespace UnitTest
{
	public static class CubicEquation7
	{
		// (別解) 負の実数解を求め、残りの二次方程式を解きます。
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
				var f = CubicEquation5.CreateFunction(c, d);
				var f1 = CubicEquation5.CreateDerivative(c);
				var x0 = -1D;
				while (f(x0) > 0) x0 *= 2;
				return NewtonMethod.Solve(f, f1, x0);
			}
		}
	}
}
