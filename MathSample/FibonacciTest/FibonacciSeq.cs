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

		public static long GetValue(int n)
		{
			var v = (Pow(φ, n) - Pow(φ_, n)) * Sqrt5 / 5;
			return (long)Round(v);
		}
	}
}
