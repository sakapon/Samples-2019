using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class ConvolutionFastTest
	{
		const int n = 1 << 16;

		static void Test(Func<long[], long[], long[]> convolution)
		{
			var f = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var g = Enumerable.Range(-5, n).Select(v => (long)v).ToArray();
			var actual = convolution(f, g);
			Assert.AreEqual(f.Length + g.Length - 1, actual.Length);
		}

		[TestMethod]
		public void Convolution_FFT()
		{
			Test(FFT.Convolution);
		}

		[TestMethod]
		public void Convolution_FFT101()
		{
			Test((f, g) => FFT101.Convolution(f.ToComplex(), g.ToComplex()).ToInt64().Resize(f.Length + g.Length - 1));
		}

		[TestMethod]
		public void Convolution_FFT102()
		{
			Test((f, g) => FFT102.Convolution(f.ToComplex(), g.ToComplex()).ToInt64().Resize(f.Length + g.Length - 1));
		}

		[TestMethod]
		public void Convolution_FFT103()
		{
			Test((f, g) => FFT103.Convolution(f.ToComplex(), g.ToComplex()).ToInt64().Resize(f.Length + g.Length - 1));
		}

		[TestMethod]
		public void Convolution_FMT()
		{
			var fmt = new FMT(n << 1);
			Test((f, g) => fmt.Convolution(f, g).Resize((n << 1) - 1));
		}

		[TestMethod]
		public void Convolution_FMT101()
		{
			var w = MPow(g, (M - 1) / (n << 1));
			var fmt = new FMT101(n << 1, M, w);
			Test((f, g) => fmt.Convolution(f, g).Resize((n << 1) - 1));
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
