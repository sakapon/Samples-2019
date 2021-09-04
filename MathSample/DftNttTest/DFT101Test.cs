using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest
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
			CollectionAssert.AreEqual(c2.ToLong(), c1.ToLong());
		}

		[TestMethod]
		public void Convolution()
		{
			var a = new Complex[] { 1, 2, 3, 4 };
			var b = new Complex[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };
			var actual = DFT101.Convolution(a, b);
			CollectionAssert.AreEqual(expected, actual.ToLong());
		}
	}
}
