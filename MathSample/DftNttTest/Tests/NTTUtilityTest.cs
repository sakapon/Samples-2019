using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class NTTUtilityTest
	{
		[TestMethod]
		public void FindMinPrime()
		{
			for (int n = 1; n > 0; n <<= 1)
			{
				var (p, d) = NTTUtility.FindMinPrime(n);
				Console.WriteLine($"n = {n}: p = {p}, d = {d}");
			}
		}

		[TestMethod]
		public void FindPrimes()
		{
			var n = 1 << 23;
			Console.WriteLine($"n = {n}");
			foreach (var (p, d) in NTTUtility.FindPrimes(n, 800000000, int.MaxValue))
			{
				Console.WriteLine($"p = {p}, d = {d}");
			}
		}

		[TestMethod]
		public void FindNthRoots()
		{
			for (int m = 2; m <= 300; m++)
			{
				FindNthRoots(m);
			}
		}

		// Z/mZ における 1 の累乗根を列挙します。
		static void FindNthRoots(long m)
		{
			if (IsPrime(m)) return;

			var l = new List<long>();
			for (long w = 2; w < m; w++)
			{
				var n = GetOrder(w);
				if (n != -1) Console.WriteLine($"m = {m}, w = {w}, n = {n}");
			}

			// NTT の十分条件を満たす場合、w の位数 n を返します。
			int GetOrder(long w)
			{
				// 1 の累乗根かどうか
				if (Gcd(w, m) != 1) return -1;

				l.Clear();
				l.Add(1);

				var t = 1L;
				while ((t = t * w % m) != 1)
					l.Add(t);

				var n = l.Count;
				// n の逆元が存在するかどうか
				if (Gcd(n, m) != 1) return -1;

				var ws = l.ToArray();
				var rn = Enumerable.Range(0, n).ToArray();
				for (int j = 1; j < n; j++)
				{
					// 一周の和が 0 かどうか
					var s = rn.Sum(k => ws[k * j % n]) % m;
					if (s != 0) return -1;
				}
				return n;
			}
		}

		static long Gcd(long a, long b) { for (long r; (r = a % b) > 0; a = b, b = r) ; return b; }

		static bool IsPrime(long n)
		{
			for (long x = 2; x * x <= n; ++x) if (n % x == 0) return false;
			return n > 1;
		}
	}
}
