using System;
using System.Linq;

class D
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();

		var ms = new long[n + 1];
		var t = 0L;
		for (var i = 1; i <= n; i++)
		{
			t = a[i - 1] - t;
			ms[i] = 2 * t;
		}
		var d = ms[n] / 2;
		Console.WriteLine(string.Join(" ", ms.Select((m, i) => m + (i % 2 == 0 ? d : -d)).Take(n)));
	}
}
