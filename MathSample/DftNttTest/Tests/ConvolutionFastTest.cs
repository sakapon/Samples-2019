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
			Assert.AreEqual(f.Length + g.Length - 2, Enumerable.Range(0, actual.Length).Last(i => actual[i] != 0));
		}

		[TestMethod]
		public void Convolution_FFT101()
		{
			Test((f, g) => FFT101.Convolution(f.ToComplex(), g.ToComplex()).ToInt64());
		}

		[TestMethod]
		public void Convolution_FFT102()
		{
			Test((f, g) => FFT102.Convolution(f.ToComplex(), g.ToComplex()).ToInt64());
		}

		[TestMethod]
		public void Convolution_FFT103()
		{
			Test((f, g) => FFT103.Convolution(f.ToComplex(), g.ToComplex()).ToInt64());
		}

		[TestMethod]
		public void Convolution_FFT202()
		{
			Test(FFT202.Convolution);
		}

		[TestMethod]
		public void Convolution_FFT203()
		{
			Test(FFT203.Convolution);
		}

		[TestMethod]
		public void Convolution_FFT302()
		{
			var fft = new FFT302();
			Test(fft.Convolution);
		}

		[TestMethod]
		public void Convolution_FNTT101()
		{
			var fmt = new FNTT101(n << 1, true);
			Test(fmt.Convolution);
		}

		[TestMethod]
		public void Convolution_FNTT202()
		{
			var t = new FNTT202(n << 1);
			Test(t.Convolution);
		}

		[TestMethod]
		public void Convolution_FMT()
		{
			Test(FMT.Convolution);
		}

		[TestMethod]
		public void Convolution_FMT302()
		{
			var fmt = new FMT302();
			Test(fmt.Convolution);
		}
	}
}
