using System;
using System.Collections.Generic;

namespace DftNttTest
{
	public static class NTTUtility
	{
		static long MPow(long b, long i, long p)
		{
			long r = 1;
			for (; i != 0; b = b * b % p, i >>= 1) if ((i & 1) != 0) r = r * b % p;
			return r;
		}

		static long[] Divisors(long n)
		{
			var r = new List<long>();
			for (long x = 1; x * x <= n; ++x) if (n % x == 0) r.Add(x);
			var i = r.Count - 1;
			if (r[i] * r[i] == n) --i;
			for (; i >= 0; --i) r.Add(n / r[i]);
			return r.ToArray();
		}

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

		// 最小の原始根
		public static long FindMinGenerator(long p)
		{
			var ds = Divisors(p - 1);

			for (long g = 1; g < p; ++g)
			{
				foreach (var d in ds)
				{
					if (d == p - 1) return g;
					if (MPow(g, d, p) == 1) break;
				}
			}
			throw new InvalidOperationException();
		}

		public static bool AssertGenerator(long p, long g)
		{
			var t = 1L;
			for (int i = 1; i < p - 1; ++i)
				if ((t = t * g % p) == 1) return false;
			return t * g % p == 1;
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
