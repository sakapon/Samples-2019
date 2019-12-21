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
			Test(9, 10, 7, 100);
			Test(1000000000, 2, 1, 100000000000);
			Test(0, 1000000000, 1000000000, 100);
			Test(254309, 1234, 56789, 314159265);

			void Test(int expected, long a, long b, long x)
			{
				Assert.AreEqual(expected, SearchSample1.BuyInteger(a, b, x));
			}
		}

		[TestMethod]
		public void GuessNumber_Random()
		{
			Test(1, 1000000000);
			Test(1000000000, 1000000000);
			for (int k = 0; k < 100; k++)
			{
				Test(random.Next(1, 1000), 1000);
				Test(random.Next(1, 1000000000), 1000000000);
			}

			void Test(int answer, int max)
			{
				Assert.AreEqual(answer, SearchSample1.GuessNumber(n => n == answer ? 0 : n > answer ? 1 : -1, max));
			}
		}

		[TestMethod]
		public void Sqrt_Random()
		{
			for (int n = 0; n < 100; n++)
			{
				Test(n, 6);
				Test(n, 9);
				Test(random.NextDouble(), 3);
				Test(random.NextDouble(), 9);
				Test(100 * random.NextDouble(), 9);
				Test(100 * random.NextDouble(), 12);
			}

			void Test(double v, int digits)
			{
				Assert.AreEqual(0, Math.Round(SearchSample1.Sqrt(v, digits) - Math.Sqrt(v), digits));
			}
		}

		[TestMethod]
		public void IndexOf_Time()
		{
			var n = 500000;
			var a = Enumerable.Range(0, n).Select(_ => random.Next(0, n)).OrderBy(x => x).ToArray();
			var l = a.ToList();

			TestHelper.MeasureTime(() => { for (int i = 0; i < n; i++) Array.BinarySearch(a, i); });
			TestHelper.MeasureTime(() => { for (int i = 0; i < n; i++) l.BinarySearch(i); });
			TestHelper.MeasureTime(() => { for (int i = 0; i < n; i++) SearchSample0.IndexOf(a, i); });
			TestHelper.MeasureTime(() => { for (int i = 0; i < n; i++) SearchSample1.IndexOf(a, i); });
			TestHelper.MeasureTime(() => { for (int i = 0; i < n; i++) SearchSample0.IndexForInsert(a, i); });
			TestHelper.MeasureTime(() => { for (int i = 0; i < n; i++) SearchSample1.IndexForInsert(a, i); });
		}
	}
}
