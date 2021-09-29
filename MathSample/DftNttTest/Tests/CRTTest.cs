using System;
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
	}
}
