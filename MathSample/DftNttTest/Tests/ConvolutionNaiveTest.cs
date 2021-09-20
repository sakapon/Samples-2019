using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class ConvolutionNaiveTest
	{
		const int n = 1 << 10;

		static void Test(Func<long[], long[], long[]> convolution)
		{
			var f = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var g = Enumerable.Range(-5, n).Select(v => (long)v).ToArray();
			var actual = convolution(f, g);
			Assert.AreEqual(f.Length + g.Length - 1, actual.Length);
		}

		[TestMethod]
		public void Convolution_DFT()
		{
			Test(DFT.Convolution);
		}

		[TestMethod]
		public void Convolution_DFT101()
		{
			Test((f, g) => DFT101.Convolution(f.ToComplex(), g.ToComplex()).ToInt64());
		}

		[TestMethod]
		public void Convolution_DFT102()
		{
			Test((f, g) => DFT102.Convolution(f.ToComplex(), g.ToComplex()).ToInt64());
		}

		[TestMethod]
		public void Convolution_NTT101()
		{
			var w = MPow(g, (M - 1) / (n << 1));
			var ntt = new NTT101(n << 1, M, w);
			Test((f, g) => ntt.Convolution(f, g).Resize((n << 1) - 1));
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
