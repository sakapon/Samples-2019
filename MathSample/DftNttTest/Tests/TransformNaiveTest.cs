﻿using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class TransformNaiveTest
	{
		const int n = 1 << 11;

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
	}
}
