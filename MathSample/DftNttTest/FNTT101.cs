using System;

namespace DftNttTest
{
	// NTT102 と FFT101
	// パラメーターを指定するためのコンストラクターを実装します。
	public class FNTT101
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

		public FNTT101(int length, long mod, long nthRoot)
		{
			n = length;
			m = mod;
			nInv = MPow(n, Totient(m) - 1);
			roots = NthRoots(n, nthRoot);
		}

		// length は 2 の冪に変更されます。
		public FNTT101(int length, bool prime, long p = 998244353, long g = 3)
		{
			n = ToPowerOf2(length);
			m = p;
			nInv = MPow(n, p - 2);
			roots = NthRoots(n, MPow(g, (p - 1) / n));
		}

		// c の長さは 2 の冪とします。
		void TransformRecursive(long[] c)
		{
			var n = c.Length;
			if (n == 1) return;

			var h = n >> 1;
			var c0 = new long[h];
			var c1 = new long[h];
			for (int k = 0; k < h; ++k)
			{
				c0[k] = c[2 * k];
				c1[k] = c[2 * k + 1];
			}

			TransformRecursive(c0);
			TransformRecursive(c1);

			for (int k = 0; k < h; ++k)
			{
				var v0 = c0[k];
				var v1 = c1[k] * roots[this.n / n * k] % m;
				c[k] = (v0 + v1) % m;
				c[k + h] = (v0 - v1 + m) % m;
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
				for (int k = 0; k < n; ++k) t[k] = t[k] * nInv % m;
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
				fa[k] = fa[k] * fb[k] % m;
			}
			return Transform(fa, true);
		}
	}
}
