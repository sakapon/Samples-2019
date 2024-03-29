﻿using System;
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
		public void FindMinGenerator()
		{
			Assert.AreEqual(1, NTTUtility.FindMinGenerator(2));
			Assert.AreEqual(2, NTTUtility.FindMinGenerator(3));
			Assert.AreEqual(2, NTTUtility.FindMinGenerator(5));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(7));
			Assert.AreEqual(2, NTTUtility.FindMinGenerator(13));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(17));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(31));
			Assert.AreEqual(6, NTTUtility.FindMinGenerator(41));
			Assert.AreEqual(5, NTTUtility.FindMinGenerator(97));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(65537));
			Assert.AreEqual(2, NTTUtility.FindMinGenerator(200003));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(7340033));
			Assert.AreEqual(15, NTTUtility.FindMinGenerator(13631489));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(104857601));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(167772161));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(469762049));
			Assert.AreEqual(11, NTTUtility.FindMinGenerator(754974721));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(998244353));
			Assert.AreEqual(3, NTTUtility.FindMinGenerator(1004535809));
			Assert.AreEqual(10, NTTUtility.FindMinGenerator(1107296257));
			Assert.AreEqual(31, NTTUtility.FindMinGenerator(2013265921));
		}

		[TestMethod]
		public void FindNthRoot()
		{
			Assert.AreEqual(6, NTTUtility.FindNthRoot(7, 1));
			Assert.AreEqual(5, NTTUtility.FindNthRoot(13, 2));
			Assert.AreEqual(2, NTTUtility.FindNthRoot(17, 3));
			Assert.AreEqual(3, NTTUtility.FindNthRoot(17, 4));
			Assert.AreEqual(-1, NTTUtility.FindNthRoot(17, 5));
		}

		[TestMethod]
		public void FindNthRoots()
		{
			for (int m = 2; m <= 200; m++)
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
				var (n, isCyclic, isCyclic2, hasNInv) = GetOrder(w);
				if (n != -1 && isCyclic && hasNInv) Console.WriteLine($"m = {m}, w = {w}, n = {n}, {(isCyclic ? 'T' : 'F')}{(isCyclic2 ? 'T' : 'F')}{(hasNInv ? 'T' : 'F')}");
			}

			// w が 1 の累乗根である場合、位数 n を返します。
			(int, bool, bool, bool) GetOrder(long w)
			{
				// 1 の累乗根かどうか
				if (Gcd(w, m) != 1) return (-1, false, false, false);

				var ws = GetRoots(w);
				var n = ws.Length;

				// n の逆元が存在するかどうか
				var hasNInv = Gcd(n, m) == 1;

				return (n, GetIsCyclic(ws), GetIsCyclic2(ws), hasNInv);
			}

			long[] GetRoots(long w)
			{
				l.Clear();
				l.Add(1);

				var t = 1L;
				while ((t = t * w % m) != 1)
					l.Add(t);
				return l.ToArray();
			}

			// 一周の和が 0 かどうか
			bool GetIsCyclic(long[] ws)
			{
				var n = ws.Length;
				var rn = Enumerable.Range(0, n).ToArray();
				for (int j = 1; j < n; j++)
				{
					var s = rn.Sum(k => ws[k * j % n]) % m;
					if (s != 0) return false;
				}
				return true;
			}

			// 一周の和の分母が存在するかどうか
			bool GetIsCyclic2(long[] ws)
			{
				var n = ws.Length;
				for (int j = 1; j < n; j++)
				{
					if (Gcd(ws[j] - 1, m) != 1) return false;
				}
				return true;
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
