using System;

namespace DftNttTest
{
	// FFT202
	// パラメーターを指定するためのコンストラクターを実装します。
	public class FNTT202
	{
		public static int ToPowerOf2(int n)
		{
			var p = 1;
			while (p < n) p <<= 1;
			return p;
		}

		// コピー先のインデックス O(n)
		// n = 8: { 0, 4, 2, 6, 1, 5, 3, 7 }
		static int[] BitReversal(int n)
		{
			var b = new int[n];
			for (int p = 1, d = n >> 1; p < n; p <<= 1, d >>= 1)
				for (int k = 0; k < p; ++k)
					b[k | p] = b[k] | d;
			return b;
		}

		long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
			return r;
		}

		// k 番目の 1 の n 乗根 (0 <= k < n/2)
		long[] NthRoots(int n, long w)
		{
			var r = new long[n >> 1];
			if (r.Length > 0) r[0] = 1;
			for (int k = 1; k < r.Length; ++k)
				r[k] = r[k - 1] * w % p;
			return r;
		}

		int n;
		public int Length => n;
		long p, nInv;
		int[] br;
		long[] roots;

		// length は 2 の冪に変更されます。
		public FNTT202(int length, long p = 998244353, long g = 3)
		{
			n = ToPowerOf2(length);
			this.p = p;
			nInv = MPow(n, p - 2);
			br = BitReversal(n);
			roots = NthRoots(n, MPow(g, (p - 1) / n));
		}

		// c の長さは 2 の冪とします。
		// h: 更新対象の長さの半分
		void TransformRecursive(long[] c, int l, int h)
		{
			if (h == 0) return;
			var d = (n >> 1) / h;

			TransformRecursive(c, l, h >> 1);
			TransformRecursive(c, l + h, h >> 1);

			for (int k = 0; k < h; ++k)
			{
				var v0 = c[l + k];
				var v1 = c[l + k + h] * roots[d * k] % p;
				c[l + k] = (v0 + v1) % p;
				c[l + k + h] = (v0 - v1 + p) % p;
			}
		}

		// 戻り値の長さは 2 の冪となります。
		public long[] Transform(long[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var t = new long[n];
			for (int k = 0; k < c.Length; ++k)
				t[br[k]] = c[k];

			TransformRecursive(t, 0, n >> 1);

			if (inverse && n > 1)
			{
				Array.Reverse(t, 1, n - 1);
				for (int k = 0; k < n; ++k) t[k] = t[k] * nInv % p;
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
				fa[k] = fa[k] * fb[k] % p;
			}
			return Transform(fa, true);
		}
	}
}
