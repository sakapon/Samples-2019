using System;
using System.Collections.Generic;

namespace UnitTest
{
	public static class BinarySearch0
	{
		// 挿入先の番号を求めます。値が重複する場合は最後尾です。すべて正の値です。
		public static int IndexForInsert(IList<int> a, int v)
		{
			int l = 0, r = a.Count, m;
			while (l < r)
			{
				m = (l + r - 1) / 2;
				if (a[m] > v) r = m;
				else l = m + 1;
			}
			return r;
		}

		// Array.BinarySearch メソッドと異なる点: 一致する値が複数存在する場合は先頭の番号。
		public static int IndexOf(IList<int> a, int v)
		{
			int l = 0, r = a.Count, m;
			while (l < r)
			{
				m = (l + r - 1) / 2;
				if (a[m] >= v) r = m;
				else l = m + 1;
			}
			return r < a.Count && a[r] == v ? r : ~r;
		}
	}
}
