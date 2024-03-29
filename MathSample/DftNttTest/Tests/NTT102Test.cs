﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class NTT102Test
	{
		static void Transform(int n, long m, long w, bool result)
		{
			var ntt = new NTT102(n, m, w);
			var f1 = Enumerable.Range(3, 5).Select(v => (long)v).ToArray();
			var f_ = ntt.Transform(f1, false);
			var f2 = ntt.Transform(f_, true).Resize(f1.Length);

			if (result) CollectionAssert.AreEqual(f1, f2);
			else CollectionAssert.AreNotEqual(f1, f2);
		}

		static void Convolution(int n, long m, long w, bool result)
		{
			var ntt = new NTT102(n, m, w);
			var f = new long[] { 2, 1, 1 };
			var g = new long[] { m - 1, m - 1, 1 };
			var expected = new long[] { m - 2, m - 3, 0, 0, 1 };
			var actual = ntt.Convolution(f, g).Resize(expected.Length);

			if (result) CollectionAssert.AreEqual(expected, actual);
			else CollectionAssert.AreNotEqual(expected, actual);
		}

		static void Test(int n, long m, long w, bool result)
		{
			Transform(n, m, w, result);
			Convolution(n, m, w, result);
		}

		static void Test(int n, long p, long g)
		{
			Test(n, p, MPow(g, (p - 1) / n, p), true);
		}

		[TestMethod]
		public void TestAll()
		{
			Test(5, 31, 2, true);
			Test(5, 25, 6, false);
			Test(5, 55, 16, false);
			Test(5, 22, 3, false);
			Test(5, 121, 3, true);
			Test(5, 341, 4, true);

			Test(6, 31, 6, true);
			Test(6, 21, 2, false);
			Test(6, 21, 5, false);
			Test(6, 35, 4, false);
			Test(6, 49, 19, true);
			Test(6, 91, 10, true);

			Test(8, 17, 2, true);
			Test(8, 51, 2, false);
			Test(8, 289, 110, true);

			var n = 1 << 8;
			Test(n, 65537, 3);
			Test(n, 998244353, 3);
			Test(n, 1107296257, 10);
			Test(n, 2013265921, 31);
		}

		static long MPow(long b, long i, long M)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
	}
}
