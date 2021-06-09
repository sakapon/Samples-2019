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
			var f = LucasNumbers.CreateSeq();

			CollectionAssert.AreEqual(expected, f[..expected.Length]);
		}

		[TestMethod]
		public void CreateSeqWithMod()
		{
			// L_n < M となる範囲
			var nLast = 43;
			var f = LucasNumbers.CreateSeq(nLast);
			var fm = LucasNumbers.CreateSeqWithMod(nLast, M);

			CollectionAssert.AreEqual(f, fm);
		}

		[TestMethod]
		public void GetValueWithMod()
		{
			var nLast = 100000;
			var fm = LucasNumbers.CreateSeqWithMod(nLast, M);

			for (int i = 0; i <= nLast; i++)
				Assert.AreEqual(fm[i], LucasNumbers.GetValueWithMod(i, M));
		}

		[TestMethod]
		public void GetValueByGeneral()
		{
			var f = LucasNumbers.CreateSeq();

			for (int i = 0; i <= 68; i++)
				Assert.AreEqual(f[i], LucasNumbers.GetValueByGeneral(i));

			for (int i = 69; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(f[i], LucasNumbers.GetValueByGeneral(i));
		}

		[TestMethod]
		public void GetValueByApprox()
		{
			var f = LucasNumbers.CreateSeq();

			// i < 2 は省略
			for (int i = 2; i <= 68; i++)
				Assert.AreEqual(f[i], LucasNumbers.GetValueByApprox(i));

			for (int i = 69; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(f[i], LucasNumbers.GetValueByApprox(i));
		}

		[TestMethod]
		public void Display_GetRawValueByApprox()
		{
			var f = LucasNumbers.CreateSeq();

			for (var i = 0; i <= N_MaxForInt64; i++)
				Console.WriteLine($"φ^{i} = {LucasNumbers.GetRawValueByApprox(i)} ≒ {f[i]}");
		}
	}
}
