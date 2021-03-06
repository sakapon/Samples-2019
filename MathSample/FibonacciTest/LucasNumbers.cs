﻿using System;
using static System.Math;

namespace FibonacciTest
{
	public static class LucasNumbers
	{
		public const int N_MaxForInt64 = 90;

		// L_90 までは Int64 の範囲内です。
		public static long[] CreateSeq(int nLast = N_MaxForInt64)
		{
			var a = new long[nLast + 1];
			a[0] = 2;
			a[1] = 1;
			for (int i = 2; i <= nLast; i++)
				a[i] = a[i - 1] + a[i - 2];
			return a;
		}

		// O(n)
		public static long[] CreateSeqWithMod(int nLast, long mod)
		{
			var a = new long[nLast + 1];
			a[0] = 2;
			a[1] = 1;
			for (int i = 2; i <= nLast; i++)
				a[i] = (a[i - 1] + a[i - 2]) % mod;
			return a;
		}

		// O(log n)
		public static long GetValueWithMod(long n, long mod)
		{
			var m = new ModMatrixOperator(mod);

			var a = new long[2, 2];
			a[0, 0] = a[0, 1] = a[1, 0] = 1;
			a = m.Pow(a, n);
			var v = m.Mul(a, new[] { 1L, 2 });
			return v[1];
		}

		// 以下、実用性はほぼありません。
		#region Lab

		static readonly double Sqrt5 = Sqrt(5);
		static readonly double φ = (1 + Sqrt5) / 2;
		static readonly double φ_ = (1 - Sqrt5) / 2;

		internal static long GetValueByGeneral(int n)
		{
			var v = Pow(φ, n) + Pow(φ_, n);
			return (long)Round(v);
		}

		// 第 1 項のみ
		internal static long GetValueByApprox(int n)
		{
			var v = GetRawValueByApprox(n);
			return (long)Round(v);
		}

		internal static double GetRawValueByApprox(int n)
		{
			return Pow(φ, n);
		}
		#endregion
	}
}
