using System;
using System.Linq;

class D
{
	static void Main()
	{
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var js = Enumerable.Range(0, a[0]).Select(i => new { i, j = Console.ReadLine().Split().Select(int.Parse).ToArray() }).OrderByDescending(_ => _.j[1]).ToDictionary(_ => _.i, _ => _.j);

		var g = 0L;
		for (var i = 1; i <= a[1]; i++)
		{
			var o = js.FirstOrDefault(_ => _.Value[0] <= i);
			if (o.Value == null) continue;
			g += o.Value[1];
			js.Remove(o.Key);
		}
		Console.WriteLine(g);
	}
}
