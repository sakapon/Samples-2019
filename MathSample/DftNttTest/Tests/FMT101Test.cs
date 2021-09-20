using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class FMT101Test
	{
		static void Transform(int n, long m, long w, bool result)
		{
			var fmt = new FMT101(n, m, w);
			var f1 = Enumerable.Range(3, 5).Select(v => (long)v).ToArray();
			var f_ = fmt.Transform(f1, false);
			var f2 = fmt.Transform(f_, true).Resize(f1.Length);

			if (result) CollectionAssert.AreEqual(f1, f2);
			else CollectionAssert.AreNotEqual(f1, f2);
		}

		static void Convolution(int n, long m, long w, bool result)
		{
			var fmt = new FMT101(n, m, w);
			var f = new long[] { 2, 1, 1 };
			var g = new long[] { m - 1, m - 1, 1 };
			var expected = new long[] { m - 2, m - 3, 0, 0, 1 };
			var actual = fmt.Convolution(f, g).Resize(expected.Length);

			if (result) CollectionAssert.AreEqual(expected, actual);
			else CollectionAssert.AreNotEqual(expected, actual);
		}

		static void Test(int n, long m, long w, bool result)
		{
			Transform(n, m, w, result);
			Convolution(n, m, w, result);
		}

		[TestMethod]
		public void TestAll()
		{
			Test(8, 17, 2, true);
			Test(8, 51, 2, false);
			Test(8, 289, 110, true);

			Test(16, 17, 3, true);
			Test(16, 51, 5, false);
			Test(16, 289, 40, true);

			var n = 1 << 8;
			var w = MPow(g, (M - 1) / n);
			Test(n, M, w, true);
		}

		const long M = 998244353, g = 3;
		static long MPow(long b, long i)
		{
			long r = 1;
			for (; i != 0; b = b * b % M, i >>= 1) if ((i & 1) != 0) r = r * b % M;
			return r;
		}
	}
}
