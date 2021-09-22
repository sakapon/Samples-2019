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
			Assert.AreEqual(f.Length + g.Length - 2, Enumerable.Range(0, actual.Length).Last(i => actual[i] != 0));
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
		public void Convolution_DFT()
		{
			Test(DFT.Convolution);
		}

		[TestMethod]
		public void Convolution_NTT102()
		{
			var ntt = new NTT102(n << 1, true);
			Test(ntt.Convolution);
		}

		[TestMethod]
		public void Convolution_NTT()
		{
			Test(NTT.Convolution);
		}
	}
}
