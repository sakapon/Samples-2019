using System;
using System.Numerics;

namespace DftNttTest
{
	public static class DFT
	{
		public static long[] ToLong(this Complex[] a) => Array.ConvertAll(a, c => (long)Math.Round(c.Real));
		public static Complex[] ToComplex(this long[] a) => Array.ConvertAll(a, c => new Complex(c, 0));

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

		// c.Length <= n であれば、復元が保証されます。
		// それ以外の場合、範囲外の係数が 0 であれば復元が保証されます。
		public static Complex[] Transform(int n, Complex[] c, bool inverse)
		{
			// 0 次元でも可。
			if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
			if (c == null) throw new ArgumentNullException(nameof(c));

			var r = new Complex[n];
			for (int k = 0; k < n; ++k)
				r[k] = inverse ? f(n, c, -k) / n : f(n, c, k);
			return r;
		}

		public static Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));
			return Transform(c.Length, c, inverse);
		}

		// n: 多項式の積の次元数
		// a.Length + b.Length - 1 <= n であれば、結果が保証されます。
		// それ以外の場合、範囲外の係数が 0 であれば結果が保証されます。
		public static Complex[] Convolution(int n, Complex[] a, Complex[] b)
		{
			// 0 次元でも可。
			if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));

			var fa = Transform(n, a, false);
			var fb = Transform(n, b, false);

			for (int k = 0; k < n; ++k)
				fa[k] *= fb[k];
			return Transform(n, fa, true);
		}

		public static Complex[] Convolution(Complex[] a, Complex[] b)
		{
			if (a == null) throw new ArgumentNullException(nameof(a));
			if (b == null) throw new ArgumentNullException(nameof(b));
			return Convolution(a.Length + b.Length - 1, a, b);
		}

		public static long[] Convolution(int n, long[] a, long[] b)
		{
			return Convolution(n, a?.ToComplex(), b?.ToComplex()).ToLong();
		}

		public static long[] Convolution(long[] a, long[] b)
		{
			return Convolution(a?.ToComplex(), b?.ToComplex()).ToLong();
		}
	}
}
