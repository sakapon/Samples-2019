using System;
using System.Collections.Generic;

namespace AlgorithmLib
{
	public static class SearchSample1
	{
		// 指定された値よりも大きい値を持つ最初のインデックスを求めます。
		// これは、挿入先のインデックスを意味します。
		public static int IndexForInsert(IList<int> a, int v) => BinarySearch.First(i => a[i] > v, 0, a.Count);

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は最初のインデックス。
		public static int IndexOf(IList<int> a, int v)
		{
			var r = BinarySearch.First(i => a[i] >= v, 0, a.Count);
			return r < a.Count && a[r] == v ? r : ~r;
		}

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は最後のインデックス。
		public static int LastIndexOf(IList<int> a, int v)
		{
			var r = BinarySearch.Last(i => a[i] <= v, -1, a.Count - 1);
			return r >= 0 && a[r] == v ? r : ~(r + 1);
		}

		// https://atcoder.jp/contests/abc146/tasks/abc146_c
		public static int BuyInteger(long a, long b, long x) => BinarySearch.Last(n => a * n + b * n.ToString().Length <= x, 0, 1000000000);

		public static double Sqrt(double v, int digits = 9) => BinarySearch.First(x => x * x >= v, Math.Min(v, 1), Math.Max(v, 1), digits);
	}
}
