using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciTest
{
	[TestClass]
	public class LucasNumbersTest
	{
		const int N_MaxForInt64 = LucasNumbers.N_MaxForInt64;
		const long M = 1000000007;

		[TestMethod]
		public void CreateSeq()
		{
			var expected = new[] { 2L, 1, 3, 4, 7, 11, 18, 29, 47, 76, 123, 199 };
			var a = LucasNumbers.CreateSeq();

			CollectionAssert.AreEqual(expected, a[..expected.Length]);
		}

		[TestMethod]
		public void CreateSeqWithMod()
		{
			// L_n < M となる範囲
			var nLast = 43;
			var a = LucasNumbers.CreateSeq(nLast);
			var am = LucasNumbers.CreateSeqWithMod(nLast, M);

			CollectionAssert.AreEqual(a, am);
		}

		[TestMethod]
		public void GetValueWithMod()
		{
			var nLast = 100000;
			var am = LucasNumbers.CreateSeqWithMod(nLast, M);

			for (int i = 0; i <= nLast; i++)
				Assert.AreEqual(am[i], LucasNumbers.GetValueWithMod(i, M));
		}

		[TestMethod]
		public void GetValueByGeneral()
		{
			var a = LucasNumbers.CreateSeq();

			for (int i = 0; i <= 68; i++)
				Assert.AreEqual(a[i], LucasNumbers.GetValueByGeneral(i));

			for (int i = 69; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(a[i], LucasNumbers.GetValueByGeneral(i));
		}

		[TestMethod]
		public void GetValueByApprox()
		{
			var a = LucasNumbers.CreateSeq();

			// i < 2 は省略
			for (int i = 2; i <= 68; i++)
				Assert.AreEqual(a[i], LucasNumbers.GetValueByApprox(i));

			for (int i = 69; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(a[i], LucasNumbers.GetValueByApprox(i));
		}

		[TestMethod]
		public void Display_GetRawValueByApprox()
		{
			var a = LucasNumbers.CreateSeq();

			for (var i = 0; i <= N_MaxForInt64; i++)
				Console.WriteLine($"φ^{i} = {LucasNumbers.GetRawValueByApprox(i)} ≒ {a[i]}");
		}
	}
}
