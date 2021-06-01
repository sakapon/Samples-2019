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
		public void CreateWithMod()
		{
			var f = FibonacciSeq.Create(44);
			var fm = FibonacciSeq.CreateWithMod(44, 1000000007);

			CollectionAssert.AreEqual(f, fm);
		}

		[TestMethod]
		public void GetValue()
		{
			var f = FibonacciSeq.Create();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(f[i], FibonacciSeq.GetValue(i));

			for (int i = 71; i <= 92; i++)
				Assert.AreNotEqual(f[i], FibonacciSeq.GetValue(i));
		}

		[TestMethod]
		public void GetValueByPhi()
		{
			var f = FibonacciSeq.Create();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(f[i], FibonacciSeq.GetValueByPhi(i));

			for (int i = 71; i <= 92; i++)
				Assert.AreNotEqual(f[i], FibonacciSeq.GetValueByPhi(i));
		}

		[TestMethod]
		public void Display_GetValueByPhiD()
		{
			var f = FibonacciSeq.Create();

			for (var i = 0; i <= 92; i++)
				Console.WriteLine($"φ^{i}/√5 = {FibonacciSeq.GetValueByPhiD(i)} ≒ {f[i]}");
		}
	}
}
