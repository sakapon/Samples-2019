using System;

namespace UnitTest
{
	public static class NewtonMethod
	{
		/// <summary>
		/// 方程式 f(x) = 0 を満たす x を Newton 法により求めます。
		/// </summary>
		/// <param name="f">対象となる関数。</param>
		/// <param name="f1">f の導関数。</param>
		/// <param name="x0">x の初期値。</param>
		/// <returns>方程式 f(x) = 0 の解。</returns>
		public static double Solve(Func<double, double> f, Func<double, double> f1, double x0)
		{
			for (var i = 0; i < 100; i++)
			{
				var temp = x0 - f(x0) / f1(x0);
				if (x0 == temp) break;
				x0 = temp;
			}
			return x0;
		}

		public static double RoundSolution(Func<double, double> f, double x)
		{
			if (f(x) == 0) return x;
			var r = Math.Round(x, 12);
			return f(r) == 0 ? r : x;
		}
	}
}
