using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var js = Enumerable.Range(0, a[0]).Select(i => Console.ReadLine().Split().Select(int.Parse).ToArray()).Where(j => j[0] <= a[1]).GroupBy(j => j[0]).ToDictionary(g => g.Key, g => g.ToArray());

		var r = 0L;
		var l = new List<int>();
		for (var i = 1; i <= a[1]; i++)
		{
			if (js.ContainsKey(i))
				foreach (var j in js[i]) l.Insert(Search(l, j[1]), j[1]);

			if (!l.Any()) continue;
			r += l.Last();
			l.RemoveAt(l.Count - 1);
		}
		Console.WriteLine(r);
	}

	static int Search(IList<int> l, int v) => l.Any() ? Search(l, v, 0, l.Count) : 0;
	static int Search(IList<int> l, int v, int start, int count)
	{
		if (count == 1) return start + (v < l[start] ? 0 : 1);
		var c = count >> 1;
		var s = start + c;
		return v < l[s] ? Search(l, v, start, c) : Search(l, v, s, count - c);
	}
}
