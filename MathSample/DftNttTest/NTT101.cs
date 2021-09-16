using System;

namespace DftNttTest
{
	public class NTT101
	{
		static long Totient(long n)
		{
			var r = n;
			for (long x = 2; x * x <= n && n > 1; ++x)
				if (n % x == 0)
				{
					r = r / x * (x - 1);
					while ((n /= x) % x == 0) ;
				}
			if (n > 1) r = r / n * (n - 1);
			return r;
		}

		int n;
		long mod, nthRoot;
		long nInv;

		public NTT101(int n, long mod, long nthRoot)
		{
			this.n = n;
			this.mod = mod;
			this.nthRoot = nthRoot;
			nInv = MPow(n, Totient(mod) - 1);
		}

		long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % mod, i >>= 1) if ((i & 1) != 0) r = r * b % mod;
			return r;
		}

		// f(ω_n^k) の値
		long f(long[] c, int k)
		{
			var r = 0L;
			for (int j = 0; j < c.Length; ++j)
				r += c[j] * MPow(nthRoot, k * j) % mod;
			return r % mod;
		}

		public long[] Transform(long[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var r = new long[n];
			for (int k = 0; k < n; ++k)
				r[k] = inverse ? f(c, n - k) * nInv % mod : f(c, k);
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
				fa[k] *= fb[k];
				fa[k] %= mod;
			}
			return Transform(fa, true);
		}
	}
}
