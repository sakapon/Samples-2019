using System;
using System.Collections.Generic;

namespace UnitTest
{
	public static class BinarySearch
	{
		/// <summary>
		/// 条件 f を満たす最初の値を探索します。
		/// [l, x) 上で false、[x, r) 上で true となる x を返します。
		/// f(l) が true のとき、l を返します。
		/// f(r - 1) が false のとき、r を返します。
		/// </summary>
		/// <param name="f">半開区間 [l, r) 上で定義される条件。</param>
		/// <param name="l">探索範囲の最小値。</param>
		/// <param name="r">探索範囲の最大値。</param>
		/// <returns>条件 f を満たす最初の値。</returns>
		public static int First(Func<int, bool> f, int l, int r)
		{
			int m;
			while (l < r) if (f(m = l + (r - l - 1) / 2)) r = m; else l = m + 1;
			return r;
		}

		/// <summary>
		/// 条件 f を満たす最後の値を探索します。
		/// (l, x] 上で true、(x, r] 上で false となる x を返します。
		/// f(r) が true のとき、r を返します。
		/// f(l + 1) が false のとき、l を返します。
		/// </summary>
		/// <param name="f">半開区間 (l, r] 上で定義される条件。</param>
		/// <param name="l">探索範囲の最小値。</param>
		/// <param name="r">探索範囲の最大値。</param>
		/// <returns>条件 f を満たす最後の値。</returns>
		public static int Last(Func<int, bool> f, int l, int r)
		{
			int m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return r;
		}

		public static double First(Func<double, bool> f, double l, double r, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
			return r;
		}

		public static double Last(Func<double, bool> f, double l, double r, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
			return r;
		}

		// 挿入先の番号を求めます。値が重複する場合は最後尾に挿入するときの番号です。すべて正の値です。
		public static int IndexForInsert(IList<int> a, int v) => First(i => a[i] > v, 0, a.Count);

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は先頭の番号。
		public static int IndexOf(IList<int> a, int v)
		{
			var r = First(i => a[i] >= v, 0, a.Count);
			return r < a.Count && a[r] == v ? r : ~r;
		}

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は最後の番号。
		public static int LastIndexOf(IList<int> a, int v)
		{
			var r = Last(i => a[i] <= v, -1, a.Count - 1);
			return r >= 0 && a[r] == v ? r : ~(r + 1);
		}

		// https://atcoder.jp/contests/abc146/tasks/abc146_c
		public static int BuyInteger(long a, long b, long x) => Last(n => a * n + b * n.ToString().Length <= x, 0, 1000000000);

		public static double Sqrt(double v, int digits = 9) => First(x => x * x >= v, Math.Min(v, 1), Math.Max(v, 1), digits);
	}
}
