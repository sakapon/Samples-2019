using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciTest
{
	[TestClass]
	public class LucasSeq3Test
	{
		const long M = 1000000007;

		[TestMethod]
		public void CreateSeqWithMod()
		{
			var nLast = 33;
			var expected = new[] { 0L, 1, 1, 1, 2, 5, 12, 28, 65, 151 };
			var am = LucasSeq3.CreateSeqWithMod(3, 2, 1, 0, 1, 1, nLast, M);

			CollectionAssert.AreEqual(expected, am[..expected.Length]);
		}

		[TestMethod]
		public void GetValueWithMod()
		{
			var nLast = 20000;
			var am = LucasSeq3.CreateSeqWithMod(3, 2, 1, 0, 1, 1, nLast, M);

			for (int i = 0; i <= nLast; i++)
				Assert.AreEqual(am[i], LucasSeq3.GetValueWithMod(3, 2, 1, 0, 1, 1, i, M));
		}
	}
}
