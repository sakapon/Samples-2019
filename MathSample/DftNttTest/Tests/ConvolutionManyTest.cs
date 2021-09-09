using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class ConvolutionManyTest
	{
		const int n1 = 1 << 10;
		const int n2 = 1 << 16;

		static void Test(int n, Func<long[], long[], long[]> convolution)
		{
			var f = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var g = Enumerable.Range(-5, n).Select(v => (long)v).ToArray();
			var actual = convolution(f, g);
			Assert.AreEqual(f.Length + g.Length - 1, actual.Length);
		}

		[TestMethod]
		public void Convolution_DFT()
		{
			Test(n1, DFT.Convolution);
		}

		[TestMethod]
		public void Convolution_DFT101()
		{
			Test(n1, (f, g) => DFT101.Convolution(f.ToComplex(), g.ToComplex()).ToInt64());
		}

		[TestMethod]
		public void Convolution_DFT102()
		{
			Test(n1, (f, g) => DFT102.Convolution(f.ToComplex(), g.ToComplex()).ToInt64());
		}

		[TestMethod]
		public void Convolution_FFT()
		{
			Test(n2, FFT.Convolution);
		}

		[TestMethod]
		public void Convolution_FFT101()
		{
			Test(n2, (f, g) => FFT101.Convolution(f.ToComplex(), g.ToComplex()).ToInt64().Resize(f.Length + g.Length - 1));
		}

		[TestMethod]
		public void Convolution_FFT102()
		{
			Test(n2, (f, g) => FFT102.Convolution(f.ToComplex(), g.ToComplex()).ToInt64().Resize(f.Length + g.Length - 1));
		}
	}
}
