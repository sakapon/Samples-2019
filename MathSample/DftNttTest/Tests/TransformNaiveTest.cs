using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class TransformNaiveTest
	{
		const int n = 1 << 11;

		static void Test(Func<Complex[], Complex[]> dft, Func<Complex[], Complex[]> idft)
		{
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var f_ = dft(f1.ToComplex());
			var f2 = idft(f_).ToInt64();
			CollectionAssert.AreEqual(f1, f2);
		}

		static void Test(Func<long[], long[]> dft, Func<long[], long[]> idft)
		{
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var f_ = dft(f1);
			var f2 = idft(f_);
			CollectionAssert.AreEqual(f1, f2);
		}

		[TestMethod]
		public void Transform_DFT()
		{
			var dft = new DFT(n);
			Test(f => dft.Transform(f, false), f => dft.Transform(f, true));
		}

		[TestMethod]
		public void Transform_DFT101()
		{
			Test(f => DFT101.Transform(f, false), f => DFT101.Transform(f, true));
		}

		[TestMethod]
		public void Transform_DFT102()
		{
			Test(f => DFT102.Transform(f, false), f => DFT102.Transform(f, true));
		}

		[TestMethod]
		public void Transform_NTT101()
		{
			var w = MPow(g, (M - 1) / n);
			var ntt = new NTT101(n, M, w);
			Test(f => ntt.Transform(f, false), f => ntt.Transform(f, true));
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
