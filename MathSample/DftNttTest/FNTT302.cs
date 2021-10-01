using System;

namespace DftNttTest
{
	// FFT302
	// n を指定せずに済む実装です。
	public class FNTT302
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
			r[0] = 1;
			for (int k = 1; k < r.Length; ++k)
				r[k] = r[k - 1] * w % p;
			return r;
		}

		public int MaxLength { get; }
		long p;
		int[] br;
		long[] roots;

		// maxLength は 2 の冪に変更されます。
		public FNTT302(int maxLength = 1 << 20, long p = 998244353, long g = 3)
		{
			MaxLength = ToPowerOf2(maxLength);
			this.p = p;
			br = BitReversal(MaxLength);
			roots = NthRoots(MaxLength, MPow(g, (p - 1) / MaxLength));
		}

		// c の長さは 2 の冪とします。
		// h: 更新対象の長さの半分
		void TransformRecursive(long[] c, int l, int h)
		{
			if (h == 0) return;
			var d = (MaxLength >> 1) / h;

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
		public long[] Transform(long[] c, bool inverse, int resultLength = -1)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var n = ToPowerOf2(resultLength == -1 ? c.Length : resultLength);
			var d = MaxLength / n;

			var t = new long[n];
			for (int k = 0; k < c.Length; ++k)
				t[br[d * k]] = c[k];

			TransformRecursive(t, 0, n >> 1);

			if (inverse && n > 1)
			{
				Array.Reverse(t, 1, n - 1);
				var nInv = MPow(n, p - 2);
				for (int k = 0; k < n; ++k) t[k] = t[k] * nInv % p;
			}
			return t;
		}

		// 戻り値の長さは |a| + |b| - 1 となります。
		public long[] Convolution(long[] a, long[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var n = a.Length + b.Length - 1;

			var fa = Transform(a, false, n);
			var fb = Transform(b, false, n);

			for (int k = 0; k < fa.Length; ++k)
			{
				fa[k] = fa[k] * fb[k] % p;
			}
			var c = Transform(fa, true);

			if (n < c.Length) Array.Resize(ref c, n);
			return c;
		}
	}
}
