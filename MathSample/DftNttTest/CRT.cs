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
			var (u, v) = ExtendedEuclid(b, r);
			return (v, u - q * v);
		}

		static BigInteger MInt(BigInteger x, long mod) => (x %= mod) < 0 ? x + mod : x;

		long mn;
		BigInteger mu, nv;

		// 前提: m と n は互いに素
		public CRT(long m, long n)
		{
			mn = m * n;
			(BigInteger u, BigInteger v) = ExtendedEuclid(m, n);
			mu = MInt(m * u, mn);
			nv = MInt(n * v, mn);
		}

		// a (mod m) かつ b (mod n) である値 (mod mn で一意)
		// 戻り値は anv + bmu
		public long Solve(long a, long b)
		{
			var x = a * nv + b * mu;
			return (long)(x % mn);
		}
	}
}
