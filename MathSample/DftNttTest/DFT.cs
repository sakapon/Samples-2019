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

		// c.Length > n の場合でも計算はできますが、復元は保証されません。
		// 範囲外の係数が 0 であれば復元が保証されます。
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
	}
}
