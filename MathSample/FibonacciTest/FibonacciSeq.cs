using System;
using static System.Math;

namespace FibonacciTest
{
	public static class FibonacciSeq
	{
		static readonly double Sqrt5 = Sqrt(5);
		static readonly double φ = (1 + Sqrt5) / 2;
		static readonly double φ_ = (1 - Sqrt5) / 2;

		// F_92 は Int64 の範囲内です。
		public static long[] Create(int max = 92)
		{
			var a = new long[max + 1];
			a[1] = 1;
			for (int i = 2; i <= max; i++)
				a[i] = a[i - 1] + a[i - 2];
			return a;
		}

		// O(n)
		public static long[] CreateWithMod(int max, long mod)
		{
			var a = new long[max + 1];
			a[1] = 1;
			for (int i = 2; i <= max; i++)
				a[i] = (a[i - 1] + a[i - 2]) % mod;
			return a;
		}

		public static long GetValue(int n, long mod)
		{
			var m = new ModMatrixOperator(mod);

			var a = new long[2, 2];
			a[0, 0] = a[0, 1] = a[1, 0] = 1;
			a = m.MPow(a, n);
			return a[1, 0];
		}

		public static long GetValueByPhi(int n)
		{
			var v = (Pow(φ, n) - Pow(φ_, n)) * Sqrt5 / 5;
			return (long)Round(v);
		}

		public static long GetValueByPhi1(int n)
		{
			var v = GetValueByPhi1D(n);
			return (long)Round(v);
		}

		public static double GetValueByPhi1D(int n)
		{
			return Pow(φ, n) * Sqrt5 / 5;
		}
	}
}
