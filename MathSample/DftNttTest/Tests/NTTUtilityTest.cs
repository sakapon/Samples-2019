using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DftNttTest.Tests
{
	[TestClass]
	public class NTTUtilityTest
	{
		[TestMethod]
		public void FindMinPrime()
		{
			for (int n = 1; n > 0; n <<= 1)
			{
				var (p, d) = NTTUtility.FindMinPrime(n);
				Console.WriteLine($"n = {n}: p = {p}, d = {d}");
			}
		}

		[TestMethod]
		public void FindPrimes()
		{
			var n = 1 << 23;
			Console.WriteLine($"n = {n}");
			foreach (var (p, d) in NTTUtility.FindPrimes(n, 800000000, int.MaxValue))
			{
				Console.WriteLine($"p = {p}, d = {d}");
			}
		}
	}
}
