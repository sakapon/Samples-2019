using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class SearchSample1Test
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
					var actual = SearchSample1.IndexForInsert(a, x);
					Assert.IsTrue(actual == 0 || a[actual - 1] <= x);
					Assert.IsTrue(actual == n || a[actual] > x);
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
					var actual = SearchSample1.IndexOf(a, x);
					if (actual >= 0)
					{
						Assert.IsTrue(actual == 0 || a[actual - 1] < x);
						Assert.IsTrue(a[actual] == x);
					}
					else
					{
						actual = ~actual;
						Assert.IsTrue(actual == 0 || a[actual - 1] < x);
						Assert.IsTrue(actual == n || a[actual] > x);
					}
				}
			}
		}

		[TestMethod]
		public void LastIndexOf_Random()
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
					var actual = SearchSample1.LastIndexOf(a, x);
					if (actual >= 0)
					{
						Assert.IsTrue(a[actual] == x);
						Assert.IsTrue(actual == n - 1 || a[actual + 1] > x);
					}
					else
					{
						actual = ~actual;
						Assert.IsTrue(actual == 0 || a[actual - 1] < x);
						Assert.IsTrue(actual == n || a[actual] > x);
					}
				}
			}
		}

		[TestMethod]
		public void BuyInteger()
		{
			Assert.AreEqual(9, SearchSample1.BuyInteger(10, 7, 100));
			Assert.AreEqual(1000000000, SearchSample1.BuyInteger(2, 1, 100000000000));
			Assert.AreEqual(0, SearchSample1.BuyInteger(1000000000, 1000000000, 100));
			Assert.AreEqual(254309, SearchSample1.BuyInteger(1234, 56789, 314159265));
		}

		[TestMethod]
		public void Sqrt_Random()
		{
			for (int n = 0; n < 100; n++)
			{
				Test(n, 6);
				Test(n, 9);
				Test(random.NextDouble(), 9);
				Test(random.NextDouble(), 12);
				Test(100 * random.NextDouble(), 6);
				Test(100 * random.NextDouble(), 9);
			}

			void Test(double v, int digits)
			{
				Assert.AreEqual(0, Math.Round(SearchSample1.Sqrt(v, digits) - Math.Sqrt(v), digits));
			}
		}
	}
}
