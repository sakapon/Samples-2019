using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class DFT101Test
	{
		[TestMethod]
		public void Transform()
		{
			var c1 = new Complex[] { 3, 4, 5, 6, 7 };
			var t = DFT101.Transform(c1, false);
			var c2 = DFT101.Transform(t, true);
			CollectionAssert.AreEqual(c2.ToInt64(), c1.ToInt64());
		}

		[TestMethod]
		public void Transform_Many()
		{
			var n = 1 << 10;
			var c1 = Enumerable.Range(3, n).Select(v => (long)v).ToArray();
			var t = DFT101.Transform(c1.ToComplex(), false);
			var c2 = DFT101.Transform(t, true);
			CollectionAssert.AreEqual(c1, c2.ToInt64());
		}

		[TestMethod]
		public void Convolution()
		{
			var a = new Complex[] { 1, 2, 3, 4 };
			var b = new Complex[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };
			var actual = DFT101.Convolution(a, b);
			CollectionAssert.AreEqual(expected, actual.ToInt64());
		}
	}
}
