using System;
using System.Numerics;

namespace DftNttTest
{
	public class CRT
	{
		// ax + by = 1 の解
		// 前提: a と b は互いに素
		public static (long x, long y) ExtendedEuclid(long a, long b)
		{
			if (b == 0) throw new ArgumentOutOfRangeException(nameof(b));
			if (b == 1) return (1, 1 - a);

			var q = Math.DivRem(a, b, out var r);
			var (x, y) = ExtendedEuclid(b, r);
			return (y, x - q * y);
		}

		long m, n, mn;
		long u, v;

		// 前提: m と n は互いに素
		public CRT(long m, long n)
		{
			this.m = m;
			this.n = n;
			mn = m * n;
			(u, v) = ExtendedEuclid(m, n);
		}

		// a mod m かつ b mod n である値 (mod mn で唯一)
		// 戻り値は anv + bmu
		public long Solve(long a, long b)
		{
			var x = (BigInteger)a * n * v + (BigInteger)b * m * u;
			var r = (long)(x % mn);
			return r < 0 ? r + mn : r;
		}
	}
}
