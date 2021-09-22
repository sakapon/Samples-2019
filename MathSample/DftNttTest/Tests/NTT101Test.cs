using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class NTT101Test
	{
		static void Transform(int n, long m, long w, long nInv)
		{
			var ntt = new NTT101(n, m, w, nInv);
			var f1 = Enumerable.Range(3, 5).Select(v => (long)v).ToArray();
			var f_ = ntt.Transform(f1, false);
			var f2 = ntt.Transform(f_, true).Resize(f1.Length);

			CollectionAssert.AreEqual(f1, f2);
		}

		static void Convolution(int n, long m, long w, long nInv)
		{
			var ntt = new NTT101(n, m, w, nInv);
			var f = new long[] { 2, 1, 1 };
			var g = new long[] { m - 1, m - 1, 1 };
			var expected = new long[] { m - 2, m - 3, 0, 0, 1 };
			var actual = ntt.Convolution(f, g).Resize(expected.Length);

			CollectionAssert.AreEqual(expected, actual);
		}

		static void Test(int n, long m, long w, long nInv)
		{
			Transform(n, m, w, nInv);
			Convolution(n, m, w, nInv);
		}

		[TestMethod]
		public void TestAll()
		{
			Test(5, 31, 2, 25);
			Test(5, 121, 3, 97);
			Test(5, 341, 4, 273);

			Test(6, 31, 6, 26);
			Test(6, 49, 19, 41);
			Test(6, 91, 10, 76);

			Test(8, 17, 2, 15);
			Test(8, 289, 110, 253);
		}
	}
}
