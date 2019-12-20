using System;

namespace AlgorithmLib
{
	/// <summary>
	/// 二分探索のためのメソッドを提供します。
	/// </summary>
	public static class BinarySearch
	{
		/// <summary>
		/// 条件 f を満たす最初の値を探索します。
		/// [l, x) 上で false、[x, r) 上で true となる x を返します。
		/// f(l) が true のとき、l を返します。
		/// f(r - 1) が false のとき、r を返します。
		/// </summary>
		/// <param name="f">半開区間 [l, r) 上で定義される条件。</param>
		/// <param name="l">探索範囲の下限。</param>
		/// <param name="r">探索範囲の上限。</param>
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
		/// <param name="l">探索範囲の下限。</param>
		/// <param name="r">探索範囲の上限。</param>
		/// <returns>条件 f を満たす最後の値。</returns>
		public static int Last(Func<int, bool> f, int l, int r)
		{
			int m;
			while (l < r) if (f(m = r - (r - l - 1) / 2)) l = m; else r = m - 1;
			return l;
		}

		/// <summary>
		/// 条件 f を満たす最初の値を指定された誤差の範囲内で探索します。
		/// (l, x) 上で false、[x, r) 上で true となる x を返します。
		/// l 近傍で true のとき、l を返します。
		/// r 近傍で false のとき、r を返します。
		/// </summary>
		/// <param name="f">開区間 (l, r) 上で定義される条件。</param>
		/// <param name="l">探索範囲の下限。</param>
		/// <param name="r">探索範囲の上限。</param>
		/// <param name="digits">誤差を表す小数部の桁数。</param>
		/// <returns>条件 f を満たす最初の値。</returns>
		public static double First(Func<double, bool> f, double l, double r, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = l + (r - l) / 2)) r = m; else l = m;
			return r;
		}

		/// <summary>
		/// 条件 f を満たす最後の値を指定された誤差の範囲内で探索します。
		/// (l, x] 上で true、(x, r) 上で false となる x を返します。
		/// r 近傍で true のとき、r を返します。
		/// l 近傍で false のとき、l を返します。
		/// </summary>
		/// <param name="f">開区間 (l, r) 上で定義される条件。</param>
		/// <param name="l">探索範囲の下限。</param>
		/// <param name="r">探索範囲の上限。</param>
		/// <param name="digits">誤差を表す小数部の桁数。</param>
		/// <returns>条件 f を満たす最後の値。</returns>
		public static double Last(Func<double, bool> f, double l, double r, int digits = 9)
		{
			double m;
			while (Math.Round(r - l, digits) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
			return l;
		}
	}
}
