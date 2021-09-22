using System;

namespace DftNttTest
{
	public class NTT101
	{
		// k 番目の 1 の n 乗根 (ω^k)
		static long[] NthRoots(int n, long m, long w)
		{
			var r = new long[n];
			r[0] = 1;
			for (int k = 1; k < n; ++k)
				r[k] = r[k - 1] * w % m;
			return r;
		}

		int n;
		long m, nInv;
		long[] roots;

		public NTT101(int n, long m, long w, long nInv)
		{
			this.n = n;
			this.m = m;
			this.nInv = nInv;
			roots = NthRoots(n, m, w);
		}

		// f(ω^k) の値
		long f(long[] c, int k)
		{
			var r = 0L;
			for (int j = 0; j < c.Length; ++j)
				r = (r + c[j] * roots[k * j % n]) % m;
			return r;
		}

		public long[] Transform(long[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var r = new long[n];
			for (int k = 0; k < n; ++k)
				r[k] = inverse ? f(c, n - k) * nInv % m : f(c, k);
			return r;
		}

		public long[] Convolution(long[] a, long[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var fa = Transform(a, false);
			var fb = Transform(b, false);

			for (int k = 0; k < n; ++k)
			{
				fa[k] = fa[k] * fb[k] % m;
			}
			return Transform(fa, true);
		}
	}
}
