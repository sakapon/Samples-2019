using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class BinarySearchTest
	{
		static readonly Random random = new Random();

		[TestMethod]
		public void Index_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Index_Random(n);
				for (int n = 1000; n < 1010; n++) Index_Random(n);
			}
		}

		static void Index_Random(int n)
		{
			var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
			for (int i = -2; i < n + 2; i++)
			{
				var expected = Array.BinarySearch(a, i);
				var actual = BinarySearch.Index(a, i);
				if (expected >= 0)
				{
					Assert.AreEqual(i, a[actual]);
					Assert.IsTrue(actual == 0 || a[actual - 1] < i);
				}
				else
				{
					Assert.AreEqual(expected, actual);
				}
			}
		}

		[TestMethod]
		public void IndexForInsert_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) IndexForInsert_Random(n);
				for (int n = 1000; n < 1010; n++) IndexForInsert_Random(n);
			}
		}

		static void IndexForInsert_Random(int n)
		{
			var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
			for (int i = -2; i < n + 2; i++)
			{
				var expected = Array.BinarySearch(a, i);
				var actual = BinarySearch.IndexForInsert(a, i);
				if (expected >= 0)
				{
					Assert.AreEqual(i, a[actual - 1]);
					Assert.IsTrue(actual == n || a[actual] > i);
				}
				else
				{
					Assert.AreEqual(~expected, actual);
				}
			}
		}
	}
}
