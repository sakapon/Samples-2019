using System;
using System.Linq;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest
{
	[TestClass]
	public class FFT101Test
	{
		[TestMethod]
		public void Transform()
		{
			var c1 = new Complex[] { 3, 4, 5, 6, 7 };
			var t = FFT101.Transform(c1, false);
			var c2 = FFT101.Transform(t, true);
			Array.Resize(ref c1, FFT101.ToPowerOf2(c1.Length));
			CollectionAssert.AreEqual(c2.ToInt64(), c1.ToInt64());
		}

		[TestMethod]
		public void Transform_Many()
		{
			var n = 1 << 18;
			var a = Enumerable.Range(3, n).ToArray();
			var c1 = Array.ConvertAll(a, x => new Complex(x, 0));
			var t = FFT101.Transform(c1, false);
			var c2 = FFT101.Transform(t, true);
			CollectionAssert.AreEqual(c2.ToInt64(), c1.ToInt64());
		}

		[TestMethod]
		public void Convolution()
		{
			var a = new Complex[] { 1, 2, 3, 4 };
			var b = new Complex[] { 5, 6, 7, 8, 9 };
			var expected = new long[] { 5, 16, 34, 60, 70, 70, 59, 36 };
			var actual = FFT101.Convolution(a, b);
			CollectionAssert.AreEqual(expected, actual.ToInt64());
		}
	}
}
