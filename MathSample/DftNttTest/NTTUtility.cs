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

		// 最小の原始根をナイーブな方法で求めます。
		public static long FindMinGenerator(long p)
		{
			for (long g = 1; g < p; ++g)
			{
				var t = 1L;
				var count = 1;
				while ((t = t * g % p) != 1) ++count;
				if (count == p - 1) return g;
			}
			throw new InvalidOperationException();
		}

		// p - 1 の素因数に 2 が多い場合
		public static long FindGenerator2(long p)
		{
			var d = p - 1;
			var c = 0;
			while (d % 2 == 0)
			{
				d >>= 1;
				++c;
			}

			for (long g = 1; g < p; ++g)
			{
				var ok = true;
				var t = 1L;

				for (int i = 0; i < d; ++i)
				{
					if ((t = t * g % p) == 1)
					{
						ok = false;
						break;
					}
				}
				if (!ok) continue;

				for (int i = 1; i < c; ++i)
				{
					if ((t = t * t % p) == 1)
					{
						ok = false;
						break;
					}
				}
				if (ok) return g;
			}
			throw new InvalidOperationException();
		}

		// 1 の 2^logn (= n) 乗根
		public static long FindNthRoot(long p, int logn)
		{
			if (logn == 1) return p - 1;

			var d = new Dictionary<long, int>();
			d[1] = 1;

			for (long w = 2; w < p; ++w)
			{
				var r = Rec(w, logn);
				if (r == logn) return w;
			}
			return -1;

			int Rec(long w, int c)
			{
				if (p - w < w) w = p - w;
				if (d.ContainsKey(w)) return d[w];

				if (c == 1) return d[w] = -1;

				var r = Rec(w * w % p, c - 1);
				if (r != -1) ++r;
				return d[w] = r;
			}
		}
	}
}
