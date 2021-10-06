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
		public void Convolution_FFT204()
		{
			Test(FFT204.Convolution);
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
			var ntt = new FNTT101(n << 1, true);
			Test(ntt.Convolution);
		}

		[TestMethod]
		public void Convolution_FNTT202()
		{
			var ntt = new FNTT202(n << 1);
			Test(ntt.Convolution);
		}

		[TestMethod]
		public void Convolution_FNTT()
		{
			Test(FNTT.Convolution);
		}

		[TestMethod]
		public void Convolution_FNTT302()
		{
			var ntt = new FNTT302();
			Test(ntt.Convolution);
		}
	}
}
