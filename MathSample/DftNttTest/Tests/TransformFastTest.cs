using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class TransformFastTest
	{
		const int n = 1 << 18;

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
		public void Transform_FFT()
		{
			var fft = new FFT(n);
			Test(f => fft.Transform(f, false), f => fft.Transform(f, true).Resize(n));
		}

		[TestMethod]
		public void Transform_FFT101()
		{
			Test(f => FFT101.Transform(f, false), f => FFT101.Transform(f, true).Resize(n));
		}

		[TestMethod]
		public void Transform_FFT102()
		{
			Test(f => FFT102.Transform(f, false), f => FFT102.Transform(f, true).Resize(n));
		}

		[TestMethod]
		public void Transform_FFT103()
		{
			Test(f => FFT103.Transform(f, false), f => FFT103.Transform(f, true).Resize(n));
		}

		[TestMethod]
		public void Transform_FFT202()
		{
			var fft = new FFT202(n);
			Test(f => fft.Transform(f, false), f => fft.Transform(f, true).Resize(n));
		}

		[TestMethod]
		public void Transform_FMT()
		{
			var fmt = new FMT(n);
			Test(f => fmt.Transform(f, false), f => fmt.Transform(f, true).Resize(n));
		}

		[TestMethod]
		public void Transform_FMT101()
		{
			var w = MPow(g, (M - 1) / n);
			var fmt = new FMT101(n, M, w);
			Test(f => fmt.Transform(f, false), f => fmt.Transform(f, true).Resize(n));
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
