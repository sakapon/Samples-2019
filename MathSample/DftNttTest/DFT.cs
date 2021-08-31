using System;
using System.Numerics;

namespace DftNttTest
{
	public static class DFT
	{
		// k-th "n-th root" of 1
		static Complex NthRoot(int n, int k)
		{
			var t = 2 * Math.PI * k / n;
			return Complex.FromPolarCoordinates(1, t);
		}

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
	}
}
