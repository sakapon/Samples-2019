using System;
using System.Numerics;

namespace DftNttTest
{
	// DFT101 の Transform メソッドを二重ループで実装します。
	public static class DFT102
	{
		// k 番目の 1 の n 乗根 (ω_n^k)
		static Complex NthRoot(int n, int k)
		{
			var t = 2 * Math.PI * k / n;
			return Complex.FromPolarCoordinates(1, t);
		}

		// f の係数が整数のとき、f^ の係数も整数になるとは限りません。
		public static Complex[] Transform(Complex[] c, bool inverse)
		{
			if (c == null) throw new ArgumentNullException(nameof(c));

			var n = c.Length;
			var r = new Complex[n];

			for (int k = 0; k < n; ++k)
			{
				for (int j = 0; j < n; ++j)
				{
					r[k] += c[j] * NthRoot(n, (inverse ? -k : k) * j);
				}
				if (inverse) r[k] /= n;
			}
			return r;
		}

		// DFT101 と同じです。
		// 戻り値の長さは |a| + |b| - 1 となります。
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
