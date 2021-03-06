﻿using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class SearchSample0Test
	{
		static readonly Random random = new Random();

		[TestMethod]
		public void IndexForInsert_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 1000; n < 1010; n++) Test(n);
			}

			void Test(int n)
			{
				var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
				for (int x = -2; x < n + 2; x++)
				{
					var expected = Array.BinarySearch(a, x);
					var actual = SearchSample0.IndexForInsert(a, x);
					if (expected >= 0)
					{
						Assert.AreEqual(x, a[actual - 1]);
						Assert.IsTrue(actual == n || a[actual] > x);
					}
					else
					{
						Assert.AreEqual(~expected, actual);
					}
				}
			}
		}

		[TestMethod]
		public void IndexOf_Random()
		{
			for (int k = 0; k < 10; k++)
			{
				for (int n = 0; n < 10; n++) Test(n);
				for (int n = 1000; n < 1010; n++) Test(n);
			}

			void Test(int n)
			{
				var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
				for (int x = -2; x < n + 2; x++)
				{
					var expected = Array.BinarySearch(a, x);
					var actual = SearchSample0.IndexOf(a, x);
					if (expected >= 0)
					{
						Assert.AreEqual(x, a[actual]);
						Assert.IsTrue(actual == 0 || a[actual - 1] < x);
					}
					else
					{
						Assert.AreEqual(expected, actual);
					}
				}
			}
		}
	}
}
