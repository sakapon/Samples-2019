﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciTest
{
	[TestClass]
	public class LucasSeqTest
	{
		const long M = 1000000007;

		[TestMethod]
		public void CreateSeqWithMod()
		{
			var nLast = 33;
			var expected = new[] { 0L, 1, 3, 7, 15, 31, 63, 127, 255 };
			var am = LucasSeq.CreateSeqWithMod(3, 2, 0, 1, nLast, M);

			CollectionAssert.AreEqual(expected, am[..expected.Length]);
		}

		[TestMethod]
		public void GetValueWithMod()
		{
			var nLast = 100000;
			var am = LucasSeq.CreateSeqWithMod(3, 2, 0, 1, nLast, M);

			for (int i = 0; i <= nLast; i++)
				Assert.AreEqual(am[i], LucasSeq.GetValueWithMod(3, 2, 0, 1, i, M));
		}

		[TestMethod]
		public void Display_GetRawValueByApprox()
		{
			var nLast = 30;
			var am = LucasSeq.CreateSeqWithMod(2, -1, 2, 2, nLast, M);

			for (var i = 0; i <= nLast; i++)
				Console.WriteLine($"{i}: {LucasSeq.GetRawValueByApprox(2, -1, 2, 2, i)} ≒ {am[i]}");
		}
	}
}
