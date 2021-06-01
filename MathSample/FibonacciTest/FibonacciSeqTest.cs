using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciTest
{
	[TestClass]
	public class FibonacciSeqTest
	{
		[TestMethod]
		public void Create()
		{
			var expected = new[] { 0L, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
			var f = FibonacciSeq.Create();

			CollectionAssert.AreEqual(expected, f[..13]);
		}

		[TestMethod]
		public void GetValue()
		{
			var f = FibonacciSeq.Create();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(f[i], FibonacciSeq.GetValue(i));

			Assert.AreNotEqual(f[71], FibonacciSeq.GetValue(71));
		}
	}
}
