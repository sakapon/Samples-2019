using System;
using System.Numerics;

namespace DftNttTest
{
	// bit-reversal permutation を利用し、再帰で実装します。
	public static class FFT102
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

		// O(n log n)
		[Obsolete]
		static int[] BitReversal_Old(int n)
		{
			var b = new int[n];
			for (int u = 1, d = n >> 1; u < n; u <<= 1, d >>= 1)
				for (int k = 0; k < n; ++k)
					if ((k & u) != 0)
						b[k] |= d;
			return b;
		}

		// k 番目の 1 の n 乗根 (ω_n^k)
		static Complex NthRoot(int n, int k)
		{
			var t = 2 * Math.PI * k / n;
			return Complex.FromPolarCoordinates(1, t);
		}

		// c の長さは 2 の冪とします。
		// h: 更新対象の長さの半分
		static void TransformRecursive(Complex[] c, int l, int h)
		{
			if (h == 0) return;

			TransformRecursive(c, l, h >> 1);
			TransformRecursive(c, l + h, h >> 1);

			for (int k = 0; k < h; ++k)
			{
				var v0 = c[l + k];
				var v1 = c[l + k + h] * NthRoot(h << 1, k);
				c[l + k] = v0 + v1;
				c[l + k + h] = v0 - v1;
			}
		}

		// 戻り値の長さは 2 の冪となります。
		public static Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var n = ToPowerOf2(c.Length);
			var br = BitReversal(n);

			var t = new Complex[n];
			for (int k = 0; k < c.Length; ++k)
				t[br[k]] = c[k];

			TransformRecursive(t, 0, n >> 1);

			if (inverse && n > 1)
			{
				Array.Reverse(t, 1, n - 1);
				for (int k = 0; k < n; ++k) t[k] /= n;
			}
			return t;
		}

		// FFT101 と同じです。
		// 戻り値の長さは 2 の冪となります。
		public static Complex[] Convolution(Complex[] a, Complex[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var n = ToPowerOf2(a.Length + b.Length - 1);
			Array.Resize(ref a, n);
			Array.Resize(ref b, n);

			var fa = Transform(a, false);
			var fb = Transform(b, false);

			for (int k = 0; k < n; ++k)
			{
				fa[k] *= fb[k];
			}
			return Transform(fa, true);
		}
	}
}
