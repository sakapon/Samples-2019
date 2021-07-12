using System;

namespace FibonacciTest
{
	// a_{n+3} = p * a_{n+2} - q * a_{n+1} + r * a_n
	public static class LucasSeq3
	{
		static long MInt(long x, long mod) => (x %= mod) < 0 ? x + mod : x;

		// O(n)
		public static long[] CreateSeqWithMod(long p, long q, long r, long a0, long a1, long a2, int nLast, long mod)
		{
			var a = new long[nLast + 1];
			a[0] = a0;
			a[1] = a1;
			a[2] = a2;
			for (int i = 3; i <= nLast; i++)
				a[i] = MInt(p * a[i - 1] - q * a[i - 2] + r * a[i - 3], mod);
			return a;
		}

		// O(log n)
		public static long GetValueWithMod(long p, long q, long r, long a0, long a1, long a2, long n, long mod)
		{
			var m = new ModMatrixOperator(mod);

			var a = new long[3, 3];
			a[0, 0] = p;
			a[0, 1] = -q;
			a[0, 2] = r;
			a[1, 0] = a[2, 1] = 1;
			a = m.Pow(a, n);
			var v = m.Mul(a, new[] { a2, a1, a0 });
			return v[2];
		}
	}
}
