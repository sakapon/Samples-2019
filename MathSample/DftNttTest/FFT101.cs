using System;
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

		// c の長さは 2 の冪とします。
		static void TransformRecursive(Complex[] c)
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

			TransformRecursive(c0);
			TransformRecursive(c1);

			for (int k = 0; k < n2; ++k)
			{
				var v0 = c0[k];
				var v1 = c1[k] * NthRoot(n, k);
				c[k] = v0 + v1;
				c[k + n2] = v0 - v1;
			}
		}

		// 戻り値の長さは 2 の冪となります。
		public static Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var n = ToPowerOf2(c.Length);
			var t = new Complex[n];
			c.CopyTo(t, 0);

			TransformRecursive(t);

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
