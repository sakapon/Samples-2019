using System;
using System.Linq;

class D
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var js = Enumerable.Range(0, a[0]).Select(i => Console.ReadLine().Split().Select(int.Parse).ToArray()).Where(j => j[0] <= a[1]).GroupBy(j => j[1]).OrderByDescending(g => g.Key).Select(g => g.OrderBy(j => j[0]).ToList()).ToList();

		var r = 0L;
		for (var i = 1; i <= a[1]; i++)
			for (var k = 0; k < js.Count; k++)
			{
				if (js[k][0][0] > i) continue;
				r += js[k][0][1];
				js[k].RemoveAt(0);
				if (js[k].Count == 0) js.RemoveAt(k);
				break;
			}
		Console.WriteLine(r);
	}
}
