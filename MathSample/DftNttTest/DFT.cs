using System;
using System.Numerics;

namespace DftNttTest
{
	public class DFT
	{
		public static long[] ToInt64(Complex[] a) => Array.ConvertAll(a, x => (long)Math.Round(x.Real));
		public static Complex[] ToComplex(long[] a) => Array.ConvertAll(a, x => new Complex(x, 0));

		// k 番目の 1 の n 乗根
		static Complex[] NthRoots(int n)
		{
			var r = new Complex[n];
			for (int k = 0; k < n; ++k)
				r[k] = Complex.FromPolarCoordinates(1, 2 * Math.PI * k / n);
			return r;
		}

		int n;
		public int Length => n;
		Complex[] roots;

		public DFT(int length)
		{
			n = length;
			roots = NthRoots(n);
		}

		// f(ω_n^k) の値
		// DFT が O(n^2) のため、k * j はオーバーフローしないと仮定しています。
		Complex f(Complex[] c, int k)
		{
			Complex r = 0;
			for (int j = 0; j < c.Length; ++j)
				r += c[j] * roots[k * j % n];
			return r;
		}

		public Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var r = new Complex[n];
			for (int k = 0; k < n; ++k)
				r[k] = inverse ? f(c, n - k) / n : f(c, k);
			return r;
		}

		public static Complex[] Convolution(Complex[] a, Complex[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var n = a.Length + b.Length - 1;
			var dft = new DFT(n);

			var fa = dft.Transform(a, false);
			var fb = dft.Transform(b, false);

			for (int k = 0; k < n; ++k)
			{
				fa[k] *= fb[k];
			}
			return dft.Transform(fa, true);
		}

		public static long[] Convolution(long[] a, long[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));
			return ToInt64(Convolution(ToComplex(a), ToComplex(b)));
		}
	}
}
