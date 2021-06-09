using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciTest
{
	[TestClass]
	public class FibonacciSeqTest
	{
		const int N_MaxForInt64 = FibonacciSeq.N_MaxForInt64;
		const long M = 1000000007;

		[TestMethod]
		public void CreateSeq()
		{
			var expected = new[] { 0L, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
			var f = FibonacciSeq.CreateSeq();

			CollectionAssert.AreEqual(expected, f[..expected.Length]);
		}

		[TestMethod]
		public void CreateSeqWithMod()
		{
			// F_n < M となる範囲
			var nLast = 44;
			var f = FibonacciSeq.CreateSeq(nLast);
			var fm = FibonacciSeq.CreateSeqWithMod(nLast, M);

			CollectionAssert.AreEqual(f, fm);
		}

		[TestMethod]
		public void GetValueWithMod()
		{
			var nLast = 100000;
			var fm = FibonacciSeq.CreateSeqWithMod(nLast, M);

			for (int i = 0; i <= nLast; i++)
				Assert.AreEqual(fm[i], FibonacciSeq.GetValueWithMod(i, M));
		}

		[TestMethod]
		public void GetValueByGeneral()
		{
			var f = FibonacciSeq.CreateSeq();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(f[i], FibonacciSeq.GetValueByGeneral(i));

			for (int i = 71; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(f[i], FibonacciSeq.GetValueByGeneral(i));
		}

		[TestMethod]
		public void GetValueByApprox()
		{
			var f = FibonacciSeq.CreateSeq();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(f[i], FibonacciSeq.GetValueByApprox(i));

			for (int i = 71; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(f[i], FibonacciSeq.GetValueByApprox(i));
		}

		[TestMethod]
		public void Display_GetRawValueByApprox()
		{
			var f = FibonacciSeq.CreateSeq();

			for (var i = 0; i <= N_MaxForInt64; i++)
				Console.WriteLine($"φ^{i} / √5 = {FibonacciSeq.GetRawValueByApprox(i)} ≒ {f[i]}");
		}
	}
}
