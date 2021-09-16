using System;
using System.Collections.Generic;

namespace DftNttTest
{
	public static class NTTUtility
	{
		static bool IsPrime(long n)
		{
			for (long x = 2; x * x <= n; ++x) if (n % x == 0) return false;
			return n > 1;
		}

		// p = dn + 1 が素数となる (p, d)
		public static (long p, long d) FindMinPrime(long n)
		{
			for (long d = 1; ; ++d)
			{
				var p = d * n + 1;
				if (IsPrime(p)) return (p, d);
			}
		}

		// p = dn + 1 が素数となる (p, d)
		public static IEnumerable<(long p, long d)> FindPrimes(long n, long min, long max)
		{
			var d = Math.Max(1, (min - 2) / n + 1);
			var p = d * n + 1;
			for (; p <= max; ++d, p += n)
			{
				if (IsPrime(p)) yield return (p, d);
			}
		}
	}
}
