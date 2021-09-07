using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class DFTTest
	{
		[TestMethod]
		public void Transform()
		{
			var n = 123;
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var dft = new DFT(n);

			// f^ は整数になるとは限りません。
			var f_ = dft.Transform(f1.ToComplex(), false);
			var f2 = dft.Transform(f_, true);
			CollectionAssert.AreEqual(f1, f2.ToInt64());
		}

		[TestMethod]
		public void Transform_Many()
		{
			var n = 1 << 10;
			var f1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var dft = new DFT(n);
			var f_ = dft.Transform(f1.ToComplex(), false);
			var f2 = dft.Transform(f_, true);
			CollectionAssert.AreEqual(f1, f2.ToInt64());
		}

		[TestMethod]
		public void Convolution()
		{
			var a = new Complex[] { 1, 2, 3, 4 };
			var b = new Complex[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };
			var actual = DFT.Convolution(a, b);
			CollectionAssert.AreEqual(expected, actual.ToInt64());
		}

		[TestMethod]
		public void Convolution_Int64()
		{
			var a = new long[] { 2, 1, 1 };
			var b = new long[] { -1, -1, 1 };
			var expected = new long[] { -2, -3, 0, 0, 1 };
			var actual = DFT.Convolution(a, b);
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
