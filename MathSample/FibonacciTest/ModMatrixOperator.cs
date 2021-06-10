using System;

namespace FibonacciTest
{
	// Uses Square Matrices
	public class ModMatrixOperator
	{
		long M;
		public ModMatrixOperator(long mod) { M = mod; }
		long MInt(long x) => (x %= M) < 0 ? x + M : x;

		public static long[,] Unit(int n)
		{
			var r = new long[n, n];
			for (var i = 0; i < n; ++i) r[i, i] = 1;
			return r;
		}

		public long[,] Pow(long[,] b, long i)
		{
			var r = Unit(b.GetLength(0));
			for (; i != 0; b = Mul(b, b), i >>= 1) if ((i & 1) != 0) r = Mul(r, b);
			return r;
		}

		public long[,] Mul(long[,] a, long[,] b)
		{
			var n = a.GetLength(0);
			var r = new long[n, n];
			for (var i = 0; i < n; ++i)
				for (var j = 0; j < n; ++j)
					for (var k = 0; k < n; ++k)
						r[i, j] = MInt(r[i, j] + a[i, k] * b[k, j]);
			return r;
		}

		public long[] Mul(long[,] a, long[] v)
		{
			var n = v.Length;
			var r = new long[n];
			for (var i = 0; i < n; ++i)
				for (var k = 0; k < n; ++k)
					r[i] = MInt(r[i] + a[i, k] * v[k]);
			return r;
		}
	}
}
