using System;
using static System.Math;

namespace FibonacciTest
{
	// a_{n+2} = p * a_{n+1} - q * a_n
	public static class LucasSeq
	{
		static long MInt(long x, long mod) => (x %= mod) < 0 ? x + mod : x;

		// O(n)
		public static long[] CreateSeqWithMod(long p, long q, long a0, long a1, int nLast, long mod)
		{
			var a = new long[nLast + 1];
			a[0] = a0;
			a[1] = a1;
			for (int i = 2; i <= nLast; i++)
				a[i] = MInt(p * a[i - 1] - q * a[i - 2], mod);
			return a;
		}

		// O(log n)
		public static long GetValueWithMod(long p, long q, long a0, long a1, int n, long mod)
		{
			var m = new ModMatrixOperator(mod);

			var a = new long[2, 2];
			a[0, 0] = p;
			a[0, 1] = -q;
			a[1, 0] = 1;
			a = m.Pow(a, n);
			var v = m.Mul(a, new[] { a1, a0 });
			return v[1];
		}

		// 以下、実用性はほぼありません。
		#region Lab

		// |β| < 1 のとき
		internal static double GetRawValueByApprox(long p, long q, long a0, long a1, int n)
		{
			var d = p * p - 4 * q;
			var α = (p + Sqrt(d)) / 2;
			var β = (p - Sqrt(d)) / 2;
			return Pow(α, n) * (a1 - a0 * β) * Sqrt(d) / d;
		}
		#endregion
	}
}
