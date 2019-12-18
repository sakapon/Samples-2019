using System;
using System.Collections.Generic;

namespace UnitTest
{
	public static class BinarySearch0
	{
		// 指定された値よりも大きい値を持つ最初のインデックスを求めます。
		// これは、挿入先のインデックスを意味します。
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

		// 指定された値以上の値を持つ最初のインデックスを求めます。
		// Array.BinarySearch メソッドと同様に、一致する値が存在しない場合はその補数を返します。
		// ただし Array.BinarySearch メソッドでは、一致する値が複数存在する場合にその最初のインデックスを返すとは限りません。
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
