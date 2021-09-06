﻿using System;
using System.Numerics;

namespace DftNttTest
{
	public static class FFT101
	{
		public static int ToPowerOf2(int n)
		{
			var p = 1;
			while (p < n) p <<= 1;
			return p;
		}

		// k 番目の 1 の n 乗根
		static Complex NthRoot(int n, int k)
		{
			var t = 2 * Math.PI * k / n;
			return Complex.FromPolarCoordinates(1, t);
		}

		// n = c.Length は 2 の冪としてください。
		static void TransformRecIn(Complex[] c)
		{
			var n = c.Length;
			if (n == 1) return;

			var n2 = n >> 1;
			var c0 = new Complex[n2];
			var c1 = new Complex[n2];
			for (int k = 0; k < n2; ++k)
			{
				c0[k] = c[2 * k];
				c1[k] = c[2 * k + 1];
			}

			TransformRecIn(c0);
			TransformRecIn(c1);

			for (int k = 0; k < n2; ++k)
			{
				var v0 = c0[k];
				var v1 = c1[k] * NthRoot(n, k);
				c[k] = v0 + v1;
				c[k + n2] = v0 - v1;
			}
		}

		// 戻り値の長さは 2 の冪となります。
		public static Complex[] TransformRec(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var n = ToPowerOf2(c.Length);
			var t = new Complex[n];
			c.CopyTo(t, 0);
			TransformRecIn(t);

			if (inverse && n > 1)
			{
				Array.Reverse(t, 1, n - 1);
				for (int k = 0; k < n; ++k) t[k] /= n;
			}
			return t;
		}

		public static Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var n = ToPowerOf2(c.Length);

			// コピー先のインデックス。
			// n = 8: { 0, 4, 2, 6, 1, 5, 3, 7 }
			var b = new int[n];
			for (int p = 1, d = n >> 1; p < n; p <<= 1, d >>= 1)
				for (int i = 0; i < p; ++i)
					b[i | p] = b[i] | d;

			// c を Resize する必要はありません。
			var t = new Complex[n];
			for (int k = 0; k < c.Length; ++k)
				t[b[k]] = c[k];

			for (int p = 1; p < n; p <<= 1)
			{
				for (int s = 0; s < n; s += p << 1)
				{
					for (int k = 0; k < p; ++k)
					{
						var v0 = t[s + k];
						var v1 = t[s + k + p] * NthRoot(p << 1, k);
						t[s + k] = v0 + v1;
						t[s + k + p] = v0 - v1;
					}
				}
			}

			if (inverse && n > 1)
			{
				Array.Reverse(t, 1, n - 1);
				for (int k = 0; k < n; ++k) t[k] /= n;
			}
			return t;
		}

		// 戻り値の長さは 2 の冪となります。
		public static Complex[] Convolution(Complex[] a, Complex[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var n = ToPowerOf2(a.Length + b.Length - 1);
			Array.Resize(ref a, n);
			Array.Resize(ref b, n);

			var fa = TransformRec(a, false);
			var fb = TransformRec(b, false);

			for (int k = 0; k < n; ++k)
			{
				fa[k] *= fb[k];
			}
			return TransformRec(fa, true);
		}
	}
}
