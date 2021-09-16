using System;
using System.Numerics;

namespace DftNttTest
{
	public static class DFT101
	{
		public static long[] ToInt64(this Complex[] a) => Array.ConvertAll(a, x => (long)Math.Round(x.Real));
		public static Complex[] ToComplex(this long[] a) => Array.ConvertAll(a, x => new Complex(x, 0));
		public static T[] Resize<T>(this T[] a, int size) { Array.Resize(ref a, size); return a; }

		// k 番目の 1 の n 乗根
		static Complex NthRoot(int n, int k)
		{
			var t = 2 * Math.PI * k / n;
			return Complex.FromPolarCoordinates(1, t);
		}

		// f(ω_n^k) の値
		static Complex f(int n, Complex[] c, int k)
		{
			Complex r = 0;
			for (int j = 0; j < c.Length; ++j)
				r += c[j] * NthRoot(n, k * j);
			return r;
		}

		public static Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var n = c.Length;
			var r = new Complex[n];
			for (int k = 0; k < n; ++k)
				r[k] = inverse ? f(n, c, -k) / n : f(n, c, k);
			return r;
		}

		public static Complex[] Convolution(Complex[] a, Complex[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var n = a.Length + b.Length - 1;
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
