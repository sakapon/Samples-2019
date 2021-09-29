using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class TransformTest
	{
		const int n = 123;

		static void Test(Func<Complex[], Complex[]> dft, Func<Complex[], Complex[]> idft)
		{
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var f_ = dft(f1.ToComplex());
			var f2 = idft(f_).ToInt64();
			if (n < f2.Length) Array.Resize(ref f2, n);
			CollectionAssert.AreEqual(f1, f2);
		}

		static void Test(Func<long[], long[]> dft, Func<long[], long[]> idft)
		{
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var f_ = dft(f1);
			var f2 = idft(f_);
			if (n < f2.Length) Array.Resize(ref f2, n);
			CollectionAssert.AreEqual(f1, f2);
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
		public void Transform_DFT()
		{
			var dft = new DFT(n);
			Test(f => dft.Transform(f, false), f => dft.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FFT101()
		{
			Test(f => FFT101.Transform(f, false), f => FFT101.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FFT102()
		{
			Test(f => FFT102.Transform(f, false), f => FFT102.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FFT103()
		{
			Test(f => FFT103.Transform(f, false), f => FFT103.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FFT202()
		{
			var fft = new FFT202(n);
			Test(f => fft.Transform(f, false), f => fft.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FFT203()
		{
			var fft = new FFT203(n);
			Test(f => fft.Transform(f, false), f => fft.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FFT302()
		{
			var fft = new FFT302(n);
			Test(f => fft.Transform(f, false), f => fft.Transform(f, true));
		}

		[TestMethod]
		public void Transform_NTT102()
		{
			var ntt = new NTT102(n, true);
			Test(f => ntt.Transform(f, false), f => ntt.Transform(f, true));
		}

		[TestMethod]
		public void Transform_NTT()
		{
			var ntt = new NTT(n);
			Test(f => ntt.Transform(f, false), f => ntt.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FMT101()
		{
			var fmt = new FMT101(n, true);
			Test(f => fmt.Transform(f, false), f => fmt.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FNTT202()
		{
			var t = new FNTT202(n);
			Test(f => t.Transform(f, false), f => t.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FMT()
		{
			var fmt = new FMT(n);
			Test(f => fmt.Transform(f, false), f => fmt.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FMT302()
		{
			var fmt = new FMT302(n);
			Test(f => fmt.Transform(f, false), f => fmt.Transform(f, true));
		}
	}
}
