using System;
using System.Collections.Generic;

namespace UnitTest
{
	public static class BinarySearch
	{
		// 条件を満たす先頭の番号を探索します。
		// f に r は渡されません。
		static int Search(Func<int, bool> f, int l, int r)
		{
			int m;
			while (l < r) if (f(m = (l + r - 1) / 2)) r = m; else l = m + 1;
			return r;
		}

		// 条件を満たす最後の番号を探索します。
		// f に l は渡されません。
		static int SearchLast(Func<int, bool> f, int l, int r)
		{
			int m;
			while (l < r) if (f(m = (l + r + 1) / 2)) l = m; else r = m - 1;
			return r;
		}

		// 挿入先の番号を求めます。値が重複する場合は最後尾に挿入するときの番号です。すべて正の値です。
		static int IndexForInsert(IList<int> a, int v) => Search(i => a[i] > v, 0, a.Count);

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は先頭の番号。
		static int Index(IList<int> a, int v)
		{
			var r = Search(i => a[i] >= v, 0, a.Count);
			return r < a.Count && a[r] == v ? r : ~r;
		}

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は最後の番号。
		static int IndexLast(IList<int> a, int v)
		{
			var r = SearchLast(i => a[i] <= v, -1, a.Count - 1);
			return r >= 0 && a[r] == v ? r : ~(r + 1);
		}
	}
}
