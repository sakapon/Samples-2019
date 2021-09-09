using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class TransformNaiveTest
	{
		const int n1 = 1 << 11;
		const int n2 = 1 << 18;

		static void Test(int n, Func<Complex[], Complex[]> dft, Func<Complex[], Complex[]> idft)
		{
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var f_ = dft(f1.ToComplex());
			var f2 = idft(f_).ToInt64();
			CollectionAssert.AreEqual(f1, f2);
		}

		[TestMethod]
		public void Transform_DFT()
		{
			var dft = new DFT(n1);
			Test(n1, f => dft.Transform(f, false), f => dft.Transform(f, true));
		}

		[TestMethod]
		public void Transform_DFT101()
		{
			Test(n1, f => DFT101.Transform(f, false), f => DFT101.Transform(f, true));
		}

		[TestMethod]
		public void Transform_DFT102()
		{
			Test(n1, f => DFT102.Transform(f, false), f => DFT102.Transform(f, true));
		}

		[TestMethod]
		public void Transform_FFT()
		{
			var fft = new FFT(n2);
			Test(n2, f => fft.Transform(f, false), f => fft.Transform(f, true).Resize(n2));
		}

		[TestMethod]
		public void Transform_FFT101()
		{
			Test(n2, f => FFT101.Transform(f, false), f => FFT101.Transform(f, true).Resize(n2));
		}

		[TestMethod]
		public void Transform_FFT102()
		{
			Test(n2, f => FFT102.Transform(f, false), f => FFT102.Transform(f, true).Resize(n2));
		}
	}
}
