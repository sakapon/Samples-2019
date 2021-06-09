using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciTest
{
	[TestClass]
	public class FibonacciNumbersTest
	{
		const int N_MaxForInt64 = FibonacciNumbers.N_MaxForInt64;
		const long M = 1000000007;

		[TestMethod]
		public void CreateSeq()
		{
			var expected = new[] { 0L, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
			var a = FibonacciNumbers.CreateSeq();

			CollectionAssert.AreEqual(expected, a[..expected.Length]);
		}

		[TestMethod]
		public void CreateSeqWithMod()
		{
			// F_n < M となる範囲
			var nLast = 44;
			var a = FibonacciNumbers.CreateSeq(nLast);
			var am = FibonacciNumbers.CreateSeqWithMod(nLast, M);

			CollectionAssert.AreEqual(a, am);
		}

		[TestMethod]
		public void GetValueWithMod()
		{
			var nLast = 100000;
			var am = FibonacciNumbers.CreateSeqWithMod(nLast, M);

			for (int i = 0; i <= nLast; i++)
				Assert.AreEqual(am[i], FibonacciNumbers.GetValueWithMod(i, M));
		}

		[TestMethod]
		public void GetValueByGeneral()
		{
			var a = FibonacciNumbers.CreateSeq();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(a[i], FibonacciNumbers.GetValueByGeneral(i));

			for (int i = 71; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(a[i], FibonacciNumbers.GetValueByGeneral(i));
		}

		[TestMethod]
		public void GetValueByApprox()
		{
			var a = FibonacciNumbers.CreateSeq();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(a[i], FibonacciNumbers.GetValueByApprox(i));

			for (int i = 71; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(a[i], FibonacciNumbers.GetValueByApprox(i));
		}

		[TestMethod]
		public void Display_GetRawValueByApprox()
		{
			var a = FibonacciNumbers.CreateSeq();

			for (var i = 0; i <= N_MaxForInt64; i++)
				Console.WriteLine($"φ^{i} / √5 = {FibonacciNumbers.GetRawValueByApprox(i)} ≒ {a[i]}");
		}
	}
}
