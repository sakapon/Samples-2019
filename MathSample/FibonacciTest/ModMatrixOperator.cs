using System;

namespace FibonacciTest
{
	public class ModMatrixOperator
	{
		long M;
		public ModMatrixOperator(long mod) { M = mod; }
		long MInt(long x) => (x %= M) < 0 ? x + M : x;

		// Square Matrices
		public static long[,] Unit(int n)
		{
			var r = new long[n, n];
			for (var i = 0; i < n; ++i) r[i, i] = 1;
			return r;
		}

		// Square Matrices
		public long[,] Pow(long[,] b, long i)
		{
			var r = Unit(b.GetLength(0));
			for (; i != 0; b = Mul(b, b), i >>= 1) if ((i & 1) != 0) r = Mul(r, b);
			return r;
		}

		public long[,] Mul(long[,] a, long[,] b)
		{
			if (a.GetLength(1) != b.GetLength(0)) throw new InvalidOperationException();
			var n = a.GetLength(0);
			var m = b.GetLength(1);
			var l = a.GetLength(1);
			var r = new long[n, m];
			for (var i = 0; i < n; ++i)
				for (var j = 0; j < m; ++j)
					for (var k = 0; k < l; ++k)
						r[i, j] = MInt(r[i, j] + a[i, k] * b[k, j]);
			return r;
		}

		public long[] Mul(long[,] a, long[] v)
		{
			if (a.GetLength(1) != v.Length) throw new InvalidOperationException();
			var n = a.GetLength(0);
			var l = v.Length;
			var r = new long[n];
			for (var i = 0; i < n; ++i)
				for (var k = 0; k < l; ++k)
					r[i] = MInt(r[i] + a[i, k] * v[k]);
			return r;
		}

		public static long[,] Transpose(long[,] a)
		{
			var n = a.GetLength(0);
			var m = a.GetLength(1);
			var r = new long[m, n];
			for (int i = 0; i < n; ++i)
				for (int j = 0; j < m; ++j)
					r[j, i] = a[i, j];
			return r;
		}
	}
}
