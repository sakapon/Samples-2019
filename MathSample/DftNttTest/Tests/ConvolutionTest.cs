﻿using System;
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
			CollectionAssert.AreEqual(expected, actual);
		}

		static void Test02(Func<long[], long[], long[]> convolution)
		{
			var f = new long[] { 1, 2, 3, 4 };
			var g = new long[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };
			var actual = convolution(f, g);
			CollectionAssert.AreEqual(expected, actual);
		}

		static void Test(Func<long[], long[], long[]> convolution)
		{
			Test01(convolution);
			Test02(convolution);
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
	}
}