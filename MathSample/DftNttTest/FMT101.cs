using System;

namespace DftNttTest
{
	public class FMT101
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

		public FMT101(int n, long mod, long nthRoot)
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

		// c の長さは 2 の冪とします。
		void TransformRecursive(long[] c)
		{
			var n = c.Length;
			if (n == 1) return;

			var n2 = n >> 1;
			var c0 = new long[n2];
			var c1 = new long[n2];
			for (int k = 0; k < n2; ++k)
			{
				c0[k] = c[2 * k];
				c1[k] = c[2 * k + 1];
			}

			TransformRecursive(c0);
			TransformRecursive(c1);

			for (int k = 0; k < n2; ++k)
			{
				var v0 = c0[k];
				var v1 = c1[k] * MPow(nthRoot, this.n / n * k) % mod;
				c[k] = (v0 + v1) % mod;
				c[k + n2] = (v0 - v1 + mod) % mod;
			}
		}

		// 戻り値の長さは 2 の冪となります。
		public long[] Transform(long[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var t = new long[n];
			c.CopyTo(t, 0);

			TransformRecursive(t);

			if (inverse && n > 1)
			{
				Array.Reverse(t, 1, n - 1);
				for (int k = 0; k < n; ++k) t[k] = t[k] * nInv % mod;
			}
			return t;
		}

		// 戻り値の長さは 2 の冪となります。
		public long[] Convolution(long[] a, long[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var fa = Transform(a, false);
			var fb = Transform(b, false);

			for (int k = 0; k < n; ++k)
			{
				fa[k] = fa[k] * fb[k] % mod;
			}
			return Transform(fa, true);
		}
	}
}
