using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class CRTTest
	{
		[TestMethod]
		public void Solve_1()
		{
			var crt1 = new CRT(3, 5);
			var crt2 = new CRT(15, 7);

			var x1 = crt1.Solve(2, 3);
			var x2 = crt2.Solve(x1, 2);

			Assert.AreEqual(23, x2);
		}

		[TestMethod]
		public void Solve_2()
		{
			var expected = 1_000000000_000000000;
			var p1 = 998244353;
			var p2 = 1107296257;

			var crt = new CRT(p1, p2);
			var actual = crt.Solve(expected % p1, expected % p2);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Convolution()
		{
			const long p1 = 998244353, g1 = 3;
			const long p2 = 1107296257, g2 = 10;

			var fntt1 = new FNTT202(8, p1, g1);
			var fntt2 = new FNTT202(8, p2, g2);

			var a = new long[] { 1000000, 1000000, 1000000 };
			var b = new long[] { 1000000, 1000000, 1000000, 1000000 };

			var c1 = fntt1.Convolution(a, b);
			var c2 = fntt2.Convolution(a, b);

			var crt = new CRT(p1, p2);
			var c = c1.Zip(c2, (x, y) => crt.Solve(x, y)).ToArray();

			var expected = new long[] { 1000000000000, 2000000000000, 3000000000000, 3000000000000, 2000000000000, 1000000000000, 0, 0 };
			CollectionAssert.AreEqual(expected, c);
		}
	}
}
