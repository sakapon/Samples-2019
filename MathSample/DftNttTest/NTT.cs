using System;

namespace DftNttTest
{
	public class NTT
	{
		const long p = 998244353, g = 3;

		public static int ToPowerOf2(int n)
		{
			var p = 1;
			while (p < n) p <<= 1;
			return p;
		}

		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
			return r;
		}

		// k 番目の 1 の n 乗根 (ω^k)
		static long[] NthRoots(int n, long w)
		{
			var r = new long[n];
			r[0] = 1;
			for (int k = 1; k < n; ++k)
				r[k] = r[k - 1] * w % p;
			return r;
		}

		int n;
		public int Length => n;
		long nInv;
		long[] roots;

		// length は 2 の冪に変更されます。
		public NTT(int length)
		{
			n = ToPowerOf2(length);
			nInv = MPow(n, p - 2);
			roots = NthRoots(n, MPow(g, (p - 1) / n));
		}

		// f(ω^k) の値
		// NTT は O(n^2) のため、k * j はオーバーフローしないと仮定しています。
		long f(long[] c, int k)
		{
			var r = 0L;
			for (int j = 0; j < c.Length; ++j)
				r = (r + c[j] * roots[k * j % n]) % p;
			return r;
		}

		public long[] Transform(long[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var r = new long[n];
			for (int k = 0; k < n; ++k)
				r[k] = inverse ? f(c, n - k) * nInv % p : f(c, k);
			return r;
		}

		// 戻り値の長さは |a| + |b| - 1 となります。
		public static long[] Convolution(long[] a, long[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var n = a.Length + b.Length - 1;
			var ntt = new NTT(n);

			var fa = ntt.Transform(a, false);
			var fb = ntt.Transform(b, false);

			for (int k = 0; k < fa.Length; ++k)
			{
				fa[k] = fa[k] * fb[k] % p;
			}
			var c = ntt.Transform(fa, true);

			if (n < c.Length) Array.Resize(ref c, n);
			return c;
		}
	}
}
