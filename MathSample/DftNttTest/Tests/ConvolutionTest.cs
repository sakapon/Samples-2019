using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class ConvolutionTest
	{
		static void Test01(Func<long[], long[], long[]> convolution)
		{
			var f = new long[] { 2, 1, 1 };
			var g = new long[] { -1, -1, 1 };
			var expected = new long[] { -2, -3, 0, 0, 1 };
			var actual = convolution(f, g);
			if (expected.Length < actual.Length) Array.Resize(ref actual, expected.Length);
			CollectionAssert.AreEqual(expected, actual);
		}

		static void Test02(Func<long[], long[], long[]> convolution)
		{
			var f = new long[] { 1, 2, 3, 4 };
			var g = new long[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };
			var actual = convolution(f, g);
			if (expected.Length < actual.Length) Array.Resize(ref actual, expected.Length);
			CollectionAssert.AreEqual(expected, actual);
		}

		static void Test(Func<long[], long[], long[]> convolution)
		{
			Test01(convolution);
			Test02(convolution);
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
			var fft = new FFT302(16);
			Test(fft.Convolution);
		}

		[TestMethod]
		public void Convolution_NTT102()
		{
			var ntt = new NTT102(16, true);
			Test02(ntt.Convolution);
		}

		[TestMethod]
		public void Convolution_NTT()
		{
			Test02(NTT.Convolution);
		}

		[TestMethod]
		public void Convolution_FNTT101()
		{
			var fmt = new FNTT101(16, true);
			Test02(fmt.Convolution);
		}

		[TestMethod]
		public void Convolution_FNTT202()
		{
			var t = new FNTT202(16);
			Test02(t.Convolution);
		}

		[TestMethod]
		public void Convolution_FMT()
		{
			Test02(FMT.Convolution);
		}

		[TestMethod]
		public void Convolution_FMT302()
		{
			var fmt = new FMT302(16);
			Test02(fmt.Convolution);
		}
	}
}
