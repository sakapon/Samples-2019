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
		public void Create()
		{
			var expected = new[] { 0L, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
			var f = FibonacciSeq.Create();

			CollectionAssert.AreEqual(expected, f[..expected.Length]);
		}

		[TestMethod]
		public void CreateWithMod()
		{
			// F_n < M となる範囲
			var nLast = 44;
			var f = FibonacciSeq.Create(nLast);
			var fm = FibonacciSeq.CreateWithMod(nLast, M);

			CollectionAssert.AreEqual(f, fm);
		}

		[TestMethod]
		public void GetValue()
		{
			var nLast = 100000;
			var fm = FibonacciSeq.CreateWithMod(nLast, M);

			for (int i = 0; i <= nLast; i++)
				Assert.AreEqual(fm[i], FibonacciSeq.GetValue(i, M));
		}

		[TestMethod]
		public void GetValueByPhi()
		{
			var f = FibonacciSeq.Create();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(f[i], FibonacciSeq.GetValueByPhi(i));

			for (int i = 71; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(f[i], FibonacciSeq.GetValueByPhi(i));
		}

		[TestMethod]
		public void GetValueByPhi1()
		{
			var f = FibonacciSeq.Create();

			for (int i = 0; i <= 70; i++)
				Assert.AreEqual(f[i], FibonacciSeq.GetValueByPhi1(i));

			for (int i = 71; i <= N_MaxForInt64; i++)
				Assert.AreNotEqual(f[i], FibonacciSeq.GetValueByPhi1(i));
		}

		[TestMethod]
		public void Display_GetValueByPhi1D()
		{
			var f = FibonacciSeq.Create();

			for (var i = 0; i <= N_MaxForInt64; i++)
				Console.WriteLine($"φ^{i}/√5 = {FibonacciSeq.GetValueByPhi1D(i)} ≒ {f[i]}");
		}
	}
}
