using System;
using System.Numerics;

namespace DftNttTest
{
	public class FFT
	{
		public static long[] ToInt64(Complex[] a) => Array.ConvertAll(a, x => (long)Math.Round(x.Real));
		public static Complex[] ToComplex(long[] a) => Array.ConvertAll(a, x => new Complex(x, 0));

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

		// k 番目の 1 の n 乗根 (0 <= k < n/2)
		static Complex[] NthRoots(int n)
		{
			var r = new Complex[n >> 1];
			for (int k = 0; k < r.Length; ++k)
				r[k] = Complex.FromPolarCoordinates(1, 2 * Math.PI * k / n);
			return r;
		}

		int n;
		public int Length => n;
		int[] br;
		Complex[] roots;

		// length は 2 の冪に変更されます。
		public FFT(int length)
		{
			n = ToPowerOf2(length);
			br = BitReversal(n);
			roots = NthRoots(n);
		}

		public Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var t = new Complex[n];
			for (int k = 0; k < c.Length; ++k)
				t[br[k]] = c[k];

			for (int p = 1, d = n >> 1; p < n; p <<= 1, d >>= 1)
			{
				for (int l = 0; l < n; l += p << 1)
				{
					for (int k = 0; k < p; ++k)
					{
						var v0 = t[l + k];
						var v1 = t[l + k + p] * roots[d * k];
						t[l + k] = v0 + v1;
						t[l + k + p] = v0 - v1;
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

		public static Complex[] Convolution(Complex[] a, Complex[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var n = a.Length + b.Length - 1;
			var fft = new FFT(n);

			var fa = fft.Transform(a, false);
			var fb = fft.Transform(b, false);

			for (int k = 0; k < fa.Length; ++k)
			{
				fa[k] *= fb[k];
			}

			var c = fft.Transform(fa, true);
			if (n < c.Length) Array.Resize(ref c, n);
			return c;
		}

		public static long[] Convolution(long[] a, long[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));
			return ToInt64(Convolution(ToComplex(a), ToComplex(b)));
		}
	}
}
