using System;

namespace DftNttTest
{
	// コンストラクターを強化した実装です。
	public class NTT102
	{
		public static int ToPowerOf2(int n)
		{
			var p = 1;
			while (p < n) p <<= 1;
			return p;
		}

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

		long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % m, i >>= 1) if ((i & 1) != 0) r = r * b % m;
			return r;
		}

		// k 番目の 1 の n 乗根 (ω^k)
		long[] NthRoots(int n, long w)
		{
			var r = new long[n];
			r[0] = 1;
			for (int k = 1; k < n; ++k)
				r[k] = r[k - 1] * w % m;
			return r;
		}

		int n;
		public int Length => n;
		long m, nInv;
		long[] roots;

		public NTT102(int length, long mod, long nthRoot)
		{
			n = length;
			m = mod;
			nInv = MPow(n, Totient(m) - 1);
			roots = NthRoots(n, nthRoot);
		}

		// length は 2 の冪に変更されます。
		public NTT102(int length, bool prime, long p = 998244353, long g = 3)
		{
			n = ToPowerOf2(length);
			m = p;
			nInv = MPow(n, p - 2);
			roots = NthRoots(n, MPow(g, (p - 1) / n));
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
